using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public enum Type { onPlayerJump, onPlayerMove, onPlayerAttack };

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //// ACTION EVENTS
    // Events based on actions the player makes. These are limited to the events that the 
    // input system listens for, such as jumping, moving, etc.
    public event Action onPlayerJump;
    public event Action onPlayerMove;
    public event Action onPlayerAttack;

    //// WAYPOINT EVENTS
    // This shouldn't be used unless you can pass data in these event calls. If not, you'd
    // have to make an event for every single waypoint in the game and that's not an option.

    //// PROGRESS EVENTS
    // These are events that could add to a progress bar, even if they are one time things.
    // For example, killing an enemy or picking up a specific powerup. 


    public void PlayerJump()
    {
        if (onPlayerJump != null) {
            onPlayerJump();
        }
    }

    public void PlayerMove()
    {
        if (onPlayerMove != null)
        {
            onPlayerMove();
        }
    }

    public void PlayerAttack()
    {
        if (onPlayerAttack != null)
        {
            onPlayerAttack();
        }
    }
}
