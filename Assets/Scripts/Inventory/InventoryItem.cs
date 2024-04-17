using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemTypeID;
    public int quantity;

    public InventoryItem(string typeID, int count)
    {
        itemTypeID = typeID;
        quantity = count;
    }
}


