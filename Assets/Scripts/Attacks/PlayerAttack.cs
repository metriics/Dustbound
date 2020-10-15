using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    private float _damage;
    private Hitbox _hitbox;
    private float _timer;

    public void Awake()
    {

    }

    override public void OnContact(Collider other)
    {
        other.gameObject.GetComponent<Enemy>().OnHit(this);
    }
}
