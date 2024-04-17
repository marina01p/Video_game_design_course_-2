using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;
    public List<InventoryItem> items = new List<InventoryItem>();

    // pentru UI
    public GameObject inventoryPanelUI;
    public GameObject[] slots;

    void Start()
    {
        inventoryPanelUI.SetActive(false);
        UpdateInventoryDisplay();
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

        UpdateInventoryDisplay();
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(string itemTypeID)
    {
        InventoryItem foundItem = items.Find(item => item.itemTypeID == itemTypeID);
    
        if (foundItem != null)
        {
            foundItem.quantity--;
            if (foundItem.quantity <= 0)
            {
                items.Remove(foundItem);
            }
        }
        UpdateInventoryDisplay();
    }

    void Update()
    {
        // pentru UI
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Inventory>().ShowInventory();
        }
    }

    void UpdateInventoryDisplay()
    {
        foreach (GameObject slot in slots)
        {
            slot.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }


        foreach (InventoryItem item in items)
        {
            GameObject slot = Array.Find(slots, slot => slot.name == item.itemTypeID);
            if (slot != null)
            {
                TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
                slotText.text = item.quantity.ToString();
            }
        }
    }

    // pentru UI
    public void ShowInventory()
    {
        inventoryPanelUI.SetActive(!inventoryPanelUI.activeSelf);
    }
}
