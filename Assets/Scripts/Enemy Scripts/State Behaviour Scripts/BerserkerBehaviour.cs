using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BerserkerBehaviour : MonoBehaviour
{
    public CharacterState state;
    public GameObject player;
    public CharacterObj character;
    public float speed = 0.3f;
    public float reach = 2.0f;
    NavMeshAgent navMeshAgent;
    public enum CharacterState { idle, attacking, moving, blocking }

    public float attackTime = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.Warp(this.transform.position);
    }

    // Update is called once per frame
    void Update()
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
        //Debug.Log(character.charName + " is idle");
        if (Vector3.Distance(this.transform.position, player.transform.position) < 15.0f)
        {
            state = CharacterState.moving;
        }
    }

    virtual public void attacking()
    {
        //Behaviour for attacking position
        //Debug.Log(character.charName + " is attacking");
        var weaponHitbox = this.transform.Find("Weapon Hitbox");
        if (weaponHitbox.gameObject.activeSelf == false)
        {
            Vector3 dir = player.transform.position - this.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = rot;
            weaponHitbox.gameObject.SetActive(true);
            attackTime = 0.0f;
        }
        else
        {
            attackTime += Time.deltaTime;
            if(attackTime >= 2.0f)
            {
                weaponHitbox.gameObject.SetActive(false);
                state = CharacterState.moving;
            }
        }
    }

    virtual public void moving(Vector3 pos)
    {
        //Behaviour for moving position
        //Debug.Log(character.charName + " is moving");
        if(Vector3.Distance(this.transform.position, player.transform.position) < reach)
        {
            state = CharacterState.attacking;
        }
        else if(Vector3.Distance(this.transform.position, player.transform.position) > 15.0f)
        {
            state = CharacterState.idle;
        }
        else
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}
