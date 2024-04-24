using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public GameObject itemTemplate;
    public Transform itemsParent;
    public List<Item> items;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddItem(Item itemToAdd)
    {
        Item foundItem = items.Find(item => item.itemName == itemToAdd.itemName);
    
        if (foundItem != null)
        {
            foundItem.count += 1;
            UpdateUIForItem(foundItem);
        }
        else
        {
            items.Add(itemToAdd);
            UpdateUIForItem(itemToAdd);
        }
    }

    void UpdateUIForItem(Item item)
    {
        Transform existingItemUI = itemsParent.Find(item.itemName);
        if (existingItemUI != null)
        {
            existingItemUI.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.count.ToString();
        }
        else
        {
            GameObject itemObj = Instantiate(itemTemplate, itemsParent);
            itemObj.name = item.itemName;
            Image itemImage = itemObj.GetComponent<Image>();
            TextMeshProUGUI itemCountText = itemObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (itemImage != null && itemCountText != null)
            {
                itemImage.sprite = item.icon;
                itemCountText.text = item.count.ToString();
            }
            itemObj.SetActive(true);
        }
    }
}
