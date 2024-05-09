using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisionHandler : MonoBehaviour
{
    public Item itemData;
    private bool canCombine = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canCombine) return;

        ItemCollisionHandler otherItemHandler = other.GetComponent<ItemCollisionHandler>();
        if (otherItemHandler != null && otherItemHandler.canCombine)
        {
            ItemCombination combination = FindCombination(itemData, otherItemHandler.itemData);
            if (combination != null)
            {
                Vector3 spawnPosition = (transform.position + other.transform.position) / 2;
                GameObject newItem = Instantiate(combination.resultPrefab, spawnPosition, Quaternion.identity);

                StartCoroutine(PreventImmediateRecombination(newItem));

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

    IEnumerator PreventImmediateRecombination(GameObject newItem)
    {
        canCombine = false;
        ItemCollisionHandler newItemHandler = newItem.GetComponent<ItemCollisionHandler>();
        if (newItemHandler != null)
        {
            newItemHandler.canCombine = false;
        }
        
        yield return new WaitForSeconds(1);

        canCombine = true;
        if (newItemHandler != null)
        {
            newItemHandler.canCombine = true;
        }
    }
}
