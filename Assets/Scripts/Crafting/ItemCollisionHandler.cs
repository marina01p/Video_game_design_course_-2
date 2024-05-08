using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisionHandler : MonoBehaviour
{
    public Item itemData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemCollisionHandler otherItemHandler = other.GetComponent<ItemCollisionHandler>();
        if (otherItemHandler != null)
        {
            ItemCombination combination = FindCombination(itemData, otherItemHandler.itemData);
            if (combination != null)
            {
                // Debug.Log("combination found");
                Vector3 spawnPosition = (transform.position + other.transform.position) / 2;
                Instantiate(combination.resultPrefab, spawnPosition, Quaternion.identity);

                Destroy(gameObject);
                Destroy(otherItemHandler.gameObject);
            }
            else
            {
                Debug.Log("no combos found");
            }
        }
    }

    private ItemCombination FindCombination(Item item1, Item item2)
    {
        if (CombinationManager.Instance == null)
        {
            return null;
        }
        return CombinationManager.Instance.FindCombination(item1, item2);
    }
}
