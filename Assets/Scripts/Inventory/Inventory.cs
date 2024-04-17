using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(string itemTypeID)
    {
        InventoryItem foundItem = items.Find(item => item.itemTypeID == itemTypeID);

        if (foundItem != null)
        {
            foundItem.quantity++;
        }
        else
        {
            items.Add(new InventoryItem(itemTypeID, 1));
        }

        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(string itemTypeID)
    {
        InventoryItem foundItem = items.Find(inventoryItem => inventoryItem.itemTypeID == itemTypeID);
        
        if (foundItem != null)
        {
            foundItem.quantity--;
            if (foundItem.quantity <= 0)
            {
                items.Remove(foundItem);
            }
        }
    }
}
