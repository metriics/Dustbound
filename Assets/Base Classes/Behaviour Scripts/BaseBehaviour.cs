using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseBehaviour : MonoBehaviour
{
    public CharacterState state;
    public GameObject player;
    public CharacterObj character;
    public float speed = 0.3f;
    float reach = 5.0f;
    NavMeshAgent navMeshAgent;
    public enum CharacterState { idle, attacking, moving }

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
        Debug.Log(character.charName + " is idle");
    }

    virtual public void attacking()
    {
        //Behaviour for attacking position
        Debug.Log(character.charName + " is attacking");
    }

    virtual public void moving(Vector3 pos)
    {
        //Behaviour for moving position
        //Debug.Log(character.charName + " is moving");
        Debug.Log(this.transform.position);
        navMeshAgent.SetDestination(player.transform.position);
        //Vector3 move = pos - this.transform.position;
        //if (move.magnitude <= reach)
        //{
        //    state = CharacterState.attacking;
        //}
        //move.Normalize();
        //this.transform.position += move * speed * Time.deltaTime;
        //player.transform.position += move * speed * Time.deltaTime;
    }
}
