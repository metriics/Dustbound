using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Threading;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerState
{
    IDLE,
    MOVING,
    ATTACKING
}

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;

    //movements
    private Input control;
    private Vector2 movement;
    private float moveSpeed = 7.0f;
    private Vector3 direction;

    //dodge roll
    private bool isDashing = false;
    private float rollTimer = 0.0f;
    private float rollCooldown = 1.0f;

    //jump
    private bool isGrounded;
    [SerializeField]
    private Rigidbody body;
    private bool isJumping = false;
    private float jumpForce = 7.0f;
    RaycastHit hit;

    //cam test stuff
    private Vector2 camVec;
    

    //attack
    [SerializeField]
    private WeaponTrigger hitbox;
    private float attackTime = 0.0f;
    private bool isAttacking = false;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        control = new Input();

        //ctx = context, can be named anything; lambda expression
        control.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        control.Gameplay.Move.canceled += ctx => movement = Vector2.zero;

        control.Gameplay.Jump.performed += ctx => Jump();
        control.Gameplay.DodgeRoll.performed += ctx => DodgeRoll();

        // cam test stuff
        control.Gameplay.Camera.performed += ctx => camVec = ctx.ReadValue<Vector2>();
        control.Gameplay.Camera.canceled += ctx => camVec = Vector2.zero;
        control.Gameplay.BasicAttack.performed += ctx => BasicAttack();
    }

    void Update()
    {
        var mouse = Mouse.current;

        direction = new Vector3(movement.x, 0.0f, movement.y);
        //direction = cam.transform.TransformDirection(direction);
        direction.y = 0.0f;
        direction.Normalize();


        Vector2 mouseD = mouse.delta.ReadValue();

        Vector3 camDir = new Vector3(mouseD.x, 0.0f, mouseD.y);

        if (camDir != Vector3.zero)
        {
            //immediate rotation
            transform.rotation = Quaternion.LookRotation(camDir);

            //slow rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
            0.1f);
        }

        if (isAttacking)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= 1.0f)
            {
                hitbox.gameObject.SetActive(false);
                attackTime = 0.0f;
                isAttacking = false;
            }
        }

    }

    void FixedUpdate()
    {

        body.MovePosition(transform.position + (direction * moveSpeed * Time.fixedDeltaTime));

        if (isJumping)
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        if (isDashing)
        {
            body.AddForce(transform.forward * 8.0f, ForceMode.Impulse);
            rollTimer = Time.fixedTime + rollCooldown;
            isDashing = false;
        }
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.7f)){
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }

    }

    void Jump()
    {
        GroundCheck();

        if (isGrounded && !isDashing && Time.time > rollTimer) 
        {
            isJumping = true;
        }
    }

    void DodgeRoll()
    {
        GroundCheck();

        if (isGrounded && !isJumping && !isDashing && Time.time > rollTimer)
        {
            isDashing = true;
        }
    }

    void BasicAttack()
    {
        if (hitbox.gameObject.activeSelf == false && isAttacking == false)
        {
            isAttacking = true;
            Quaternion rot = Quaternion.LookRotation(transform.position);
            hitbox.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }


    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }


}
