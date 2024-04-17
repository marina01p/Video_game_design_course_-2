using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;
    public List<InventoryItem> items = new List<InventoryItem>();

    // pentru UI
    public GameObject inventoryUI;

    void Start()
    {
        inventoryUI.SetActive(false);
    }

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Inventory>().ToggleInventory();
        }
    }

    // pentru UI
    public void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
