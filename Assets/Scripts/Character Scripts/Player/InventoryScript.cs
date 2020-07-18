using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    private List<ItemSlot> _inventory = new List<ItemSlot>();

    public void AddItem(ItemAsset _item)
    {
        if (_inventory.Count < 9)
        {
            _inventory.Add(new ItemSlot(_item));
            Debug.Log(_item.name + " item added.");
        } 
        else 
        {
            Debug.Log("Inventory is full.");
        }
    }

    public void RemoveItem(int _slot)
    {
        _inventory.RemoveAt(_slot);
    }
}

[Serializable]
public class ItemSlot
{
    private ItemAsset item;
    public ItemSlot(ItemAsset _item)
    {
        item = _item;
    }

}
