using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : BaseBehaviour
{
    public override void idle()
    {
        base.idle();
        Debug.Log(character.charDesc);
    }

    public override void attacking()
    {
        base.attacking();
        Debug.Log(character.charDesc);
    }

    public override void moving()
    {
        base.moving();
        Debug.Log(character.charDesc);
    }
}
