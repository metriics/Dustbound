using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Item", menuName = "Asset/Items/Healing")]
public class HealingItem : ItemAsset
{
    private int healingValue;
    //private Status healingStatus;
    
    public void Awake()
    {
       itemType = ItemType.Healing;
    }

}
