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

    // Update is called once per frame
    void Awake()
    {
    }
}
