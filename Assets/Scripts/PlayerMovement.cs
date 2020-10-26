﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Input playerInput;
    private Vector2 mouseVec;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Vector2 moveVec;
    private Vector3 prevVec;
    private Vector3 movement;

    private bool isJumping = false;
    private bool isGrounded = false;

    private float gravity = -9.8f;

    public float sensitivity = 10.0f;
    public GameObject followTarget; // we will use this for slight Y axis camera rotation
    public float moveSpeed = 10.0f;

    public Transform groundCheckLocation;
    public float groundDist = 0.01f;
    public LayerMask groundMask;

    private void Awake()
    {
        playerInput = new Input();

        playerInput.Gameplay.Move.performed += ctx => moveVec = ctx.ReadValue<Vector2>();
        playerInput.Gameplay.Move.canceled += ctx => moveVec = Vector2.zero;

        playerInput.Gameplay.Camera.performed += ctx => mouseVec = ctx.ReadValue<Vector2>();
        playerInput.Gameplay.Camera.canceled += ctx => mouseVec = Vector2.zero;

        playerInput.Gameplay.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private Vector3 GetMoveVector()
    {
        Vector3 direction = new Vector3(moveVec.x, 0.0f, moveVec.y);
        direction = transform.TransformDirection(direction);
        direction.y = 0.0f;

        movement = direction * moveSpeed * Time.deltaTime;

        if (isGrounded)
        {
            movement.y = 0.0f;
        }
        else
        {
            movement.y += gravity * moveSpeed * Time.deltaTime;
        }

        return movement;
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckLocation.position, groundDist, groundMask);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            Debug.Log("Jump");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = mouseVec.x * sensitivity * Time.deltaTime;
        float mouseY = mouseVec.y * sensitivity * Time.deltaTime;

        xRotation += mouseX;
        yRotation += mouseY;

        // clamp y rotation here so you cant look upside down with a broken neck
        yRotation = Mathf.Clamp(yRotation, 0.0f, 25.0f);

        transform.localRotation = Quaternion.Euler(0.0f, xRotation, 0.0f);
        followTarget.transform.localRotation = Quaternion.Euler(yRotation, 0.0f, 0.0f);


        GroundCheck();

        Vector3 curMoveVector = GetMoveVector();

        if (isJumping)
        {
            curMoveVector.y = 10.0f;
            isJumping = false;
        }

        controller.Move(curMoveVector);
        

        prevVec = curMoveVector;
    }
}