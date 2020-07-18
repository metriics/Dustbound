using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public CharacterState state;
    public Character character;

    public enum CharacterState { idle, attacking, moving }

    // Start is called before the first frame update
    void Start()
    {
        
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
            moving();
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

    virtual public void moving()
    {
        //Behaviour for moving position
        Debug.Log(character.charName + " is moving");
    }
}
