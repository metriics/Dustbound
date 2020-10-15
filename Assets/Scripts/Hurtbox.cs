using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public Collider _collider;
    private CollisionState hurtboxState = CollisionState.ON;

    // Start is called before the first frame update
    private void OnHit(Attack _attack)
    {
        
    }
}
