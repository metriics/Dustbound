using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum CharacterStates { IDLE, ATTACKING, WALKING, HIT}

public class Character : MonoBehaviour
{
    public CharacterStates state;
    public string charName;
    public string charDesc;
    public int baseHealth;
    public int baseDmg;
    public int baseArmour;

    public virtual void Attack()
    {
        //Overide this method for specific characters
    }

}
