using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemTypeID;
    public int quantity;
    public Sprite itemIcon;

    public InventoryItem(string typeID, int count, Sprite icon)
    {
        itemTypeID = typeID;
        quantity = count;
        itemIcon = icon;
    }
}





