using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum ItemType
{
    Healing,
    Buff,
    Treasure,
    Artifact,
    Null
}

public abstract class ItemAsset : ScriptableObject
{
    private int _ID;
    private ItemType _itemType;
    private Sprite _sprite;
    [TextArea(10, 15)]
    private string _desciption;


    public ItemType itemType { get { return _itemType; } set { _itemType = value; } }
    public int ID { get { return _ID; } set { _ID = value; } }

}
