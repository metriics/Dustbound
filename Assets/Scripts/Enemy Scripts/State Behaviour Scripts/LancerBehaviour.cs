using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerBehaviour : BerserkerBehaviour
{
    public float blockRadius = 10.0f;
    public float blockTimer = 0.0f;
    public bool hit = false;

    public override void idle()
    {
        base.idle();
    }

    public override void attacking()
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
            if (attackTime >= 2.0f)
            {
                weaponHitbox.gameObject.SetActive(false);
                speed = 5.0f;
                state = CharacterState.moving;
            }
        }
    }

    public void blocking(Vector3 pos)
    {
        speed = 2.0f;
        moving(pos);
        if (Vector3.Distance(this.transform.position, player.transform.position) < reach)
        {
            blockTimer += Time.deltaTime;
            if (hit)
            {
                blockTimer = 0.0f;
            }
            else if (blockTimer >= 5.0f)
            {
                state = CharacterState.attacking;
                blockTimer = 0.0f;
            }
        }
        else
        {
            blockTimer = 0.0f;
        }
    }

    public override void moving(Vector3 pos)
    {
        base.moving(pos);
        if(Vector3.Distance(this.transform.position, player.transform.position) < blockRadius)
        {
            state = CharacterState.blocking;
        }
    }
}
