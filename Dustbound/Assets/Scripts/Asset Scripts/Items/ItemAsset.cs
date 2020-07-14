using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Healing,
    Buff,
    Weapon,
    Null
}

public abstract class ItemAsset : ScriptableObject 
{
    public int ID;
    public Sprite displaySprite;
    [TextArea(10,15)]
    public string desciption;

}
