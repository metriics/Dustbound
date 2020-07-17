using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if any errors with items (i.e. bugs or undocumented behaviour), refer to Null Item
public class NullItem : ItemAsset
{
    public void Awake()
    {
        ID = -1;
        itemType = ItemType.Null;
    }
}
