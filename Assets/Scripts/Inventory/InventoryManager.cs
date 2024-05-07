using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject itemTemplate;
    public Transform itemsParent;
    public static bool CanAddItems = false;
    public List<Item> items = new List<Item>();
    private Dictionary<Item, GameObject> itemUIs = new Dictionary<Item, GameObject>();

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

    void Start()
    {
        StartCoroutine(EnableCollectionAfterDelay());
    }

    IEnumerator EnableCollectionAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);
        CanAddItems = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(EnableCollectionAfterDelay());
    }

    public void UpdateUI()
    {
        List<Item> itemsToRemove = new List<Item>();
        foreach (var pair in itemUIs)
        {
            if (!items.Contains(pair.Key) || pair.Key.count == 0)
            {
                Destroy(pair.Value);
                itemsToRemove.Add(pair.Key);
            }
        }

        foreach (Item item in itemsToRemove)
        {
            itemUIs.Remove(item);
        }

        foreach (Item item in items)
        {
            GameObject uiObject;
            if (itemUIs.TryGetValue(item, out uiObject))
            {
                InventoryItemUI ui = uiObject.GetComponent<InventoryItemUI>();
                ui.SetItemData(item);
            }
            else
            {
                GameObject newItemUI = Instantiate(itemTemplate, itemsParent);
                InventoryItemUI newUI = newItemUI.GetComponent<InventoryItemUI>();
                newUI.SetItemData(item);
                newItemUI.SetActive(true);
                itemUIs[item] = newItemUI;
            }
        }
    }

    public void AddItem(Item itemToAdd)
    {
        if (!CanAddItems) return;

        Item existingItem = items.Find(item => item.itemName == itemToAdd.itemName);
        if (existingItem != null)
        {
            existingItem.count += 1;
        }
        else
        {
            items.Add(itemToAdd);
            itemToAdd.count = 1;
        }
        UpdateUI();
    }

    public void DropItemToWorld(Item item, Vector2 dropPosition)
    {
        if (item != null && item.worldPrefab != null && item.count > 0)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(dropPosition.x, dropPosition.y, Camera.main.nearClipPlane));
            Instantiate(item.worldPrefab, worldPosition, Quaternion.identity);
            item.count--;

            if (item.count <= 0)
            {
                items.Remove(item);
            }

            UpdateUI();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
