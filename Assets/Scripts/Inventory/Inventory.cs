using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanelUI;
    public GameObject[] slots;

    public List<InventoryItem> items = new List<InventoryItem>(); 

    void Start()
    {
        inventoryPanelUI.SetActive(false);
        UpdateInventoryDisplay();
    }

public void AddItem(string itemTypeID, Sprite itemIcon)
{
    InventoryItem foundItem = items.Find(item => item.itemTypeID == itemTypeID);
    if (foundItem != null)
    {
        foundItem.quantity++;
        Debug.Log("Item added: " + itemTypeID + " new count: " + foundItem.quantity);
    }
    else
    {
        items.Add(new InventoryItem(itemTypeID, 1, itemIcon));
        Debug.Log("New item added: " + itemTypeID);
    }

    UpdateInventoryDisplay();
}


    void UpdateInventoryDisplay()
    {
        foreach (GameObject slot in slots)
        {
            TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
            Image slotImage = slot.GetComponentInChildren<Image>();

            slotText.text = "";
            slotImage.sprite = null;
            slotImage.enabled = false;

            InventoryItem item = items.Find(i => i.itemTypeID == slot.name);
            if (item != null && item.quantity > 0)
            {
                slotText.text = item.quantity.ToString();
                slotImage.sprite = item.itemIcon;
                slotImage.enabled = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanelUI.SetActive(!inventoryPanelUI.activeSelf);  // Toggle inventory UI
        }
    }

    public void ShowInventory()
    {
        inventoryPanelUI.SetActive(!inventoryPanelUI.activeSelf);  // Toggle inventory panel visibility
    }
}
