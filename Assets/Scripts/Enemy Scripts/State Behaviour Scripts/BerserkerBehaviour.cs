using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BerserkerBehaviour : MonoBehaviour
{
    public enum CharacterState { idle, attacking, blocking, moving } //Enemy State Enum

    public float speed = 5.0f; //Base speed for enemy
    public float reach = 2.0f; //Attack Range for enemy
    protected Enemy enemy; //Stats from enemy class
    protected CharacterState state = CharacterState.idle;
    protected GameObject player;
    protected NavMeshAgent navMeshAgent;
    protected float attackTime = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.Warp(this.transform.position);
    }

    void Start()
    {
        player = this.transform.GetComponentInParent<EnemyManager>().player;// EnemyManager.Instance.GetPlayer();
        enemy = this.transform.GetComponent<Enemy>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if(state == CharacterState.idle)
        {
            idle();
        }
        else if(state == CharacterState.attacking)
        {
            attacking();
        }
        else if(state == CharacterState.moving)
        {
            moving(player.transform.position);
        }
    }

    virtual public void idle()
    {
        //Behaviour for idle position
        navMeshAgent.speed = 0.0f;
        navMeshAgent.SetDestination(this.transform.position);
        if (Vector3.Distance(this.transform.position, player.transform.position) < 20.0f)
        {
            state = CharacterState.moving;
        }
    }

    virtual public void attacking()
    {
        //Behaviour for attacking position
        var weaponHitbox = this.transform.Find("Weapon Hitbox");
        attackTime += Time.deltaTime;
        if(attackTime >= 4.0f)
        {
            //Done attack
            weaponHitbox.gameObject.SetActive(false);
            state = CharacterState.moving;
            attackTime = 0.0f;
        }
        else if (attackTime >= 2.0f)
        {
            //Start Attacking
            weaponHitbox.gameObject.SetActive(true);
        }
        else if (weaponHitbox.gameObject.activeSelf == false)
        {
            //Preping attack
            Vector3 dir = player.transform.position - this.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            rot.x = 0.0f;
            rot.z = 0.0f;
            transform.rotation = rot;
        }
    }

    virtual public void moving(Vector3 pos)
    {
        //Behaviour for moving position
        if(Vector3.Distance(this.transform.position, player.transform.position) < reach)
        {
            //If player is within reach start attacking
            state = CharacterState.attacking;
        }
        else if(Vector3.Distance(this.transform.position, player.transform.position) > 20.0f)
        {
            //If player is out of sight/distance go idle
            state = CharacterState.idle;
        }
        else
        {
            //Move towards player
            navMeshAgent.speed = speed;
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}
