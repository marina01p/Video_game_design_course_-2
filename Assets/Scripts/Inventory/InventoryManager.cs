using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public static bool CanAddItems = false;
    public GameObject itemTemplate;
    public Transform itemsParent;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayItemAddition());
    }

    IEnumerator DelayItemAddition()
    {
        ResetInventory();
        yield return new WaitForSeconds(2);
        CanAddItems = true;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ResetInventory()
    {
        items.Clear();
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject newItemUI = Instantiate(itemTemplate, itemsParent);
            InventoryItemUI itemUIScript = newItemUI.GetComponent<InventoryItemUI>();
            itemUIScript.SetItemData(item);
            newItemUI.SetActive(true);
        }
    }

    public void AddItem(Item itemToAdd)
    {
        // if (!CanAddItems)
        // {
        //     return;
        // }

        Item existingItem = items.Find(item => item.itemName == itemToAdd.itemName);
        if (existingItem != null)
        {
            existingItem.count += 1;
        }
        else
        {
            itemToAdd.count = 1;
            items.Add(itemToAdd);
        }

        UpdateUI();
    }
    public void DropItemToWorld(Item item, Vector2 dropPosition)
    {
        if (item != null && item.worldPrefab != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(dropPosition.x, dropPosition.y, Camera.main.nearClipPlane));
            worldPosition.z = 0;
            Instantiate(item.worldPrefab, worldPosition, Quaternion.identity);

            item.count--;
            if (item.count <= 0)
            {
                items.Remove(item);
            }

            UpdateUI();
        }
    }
}
