using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CollisionState
{
    OFF,
    ON,
    COLLIDING
}

public class Hitbox : MonoBehaviour
{
    List<Collider> hitColliders;
    private CollisionState _state;

    public LayerMask mask;

    private void Start()
    {
        _state = CollisionState.OFF;
    }


    // Update is called once per frame
    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, 
            transform.localScale / 2, transform.rotation, mask);

        if (colliders.Length > 0)
        {
            //TODO: hit code, determine interaction
            Debug.Log("hit");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawCube(Vector3.zero, transform.position);
    }
    
    private void gizmoColour()
    {
        switch (_state)
        {
            case CollisionState.OFF:
            {
                Gizmos.color = Color.magenta;
                break;
            }
            case CollisionState.ON:
            {
                Gizmos.color = Color.cyan;
                break;
            }
            case CollisionState.COLLIDING:
            {
                Gizmos.color = Color.red;
                break;
            }

        }
    }

}
