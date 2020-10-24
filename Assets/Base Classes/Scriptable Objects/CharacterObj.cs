using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterObj : ScriptableObject
{
    public Who who;
    public string charName;
    public string charDesc;
    public int baseHealth;
    public int baseDmg;
    public int baseArmour;

    public enum Who { player, enemy, npc }
}
