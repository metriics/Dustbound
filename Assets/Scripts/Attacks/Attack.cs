using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

abstract public class Attack 
{
    private float _damage;
    private Hitbox _hitbox;
    private float _timer;

    public virtual void OnContact(Collider other)
    {
    }
}
