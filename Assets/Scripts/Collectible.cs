using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour
{
    public Item itemData;

    void Start()
    {
        StartCoroutine(ActivateCollectible());
    }

    IEnumerator ActivateCollectible()
    {
        yield return new WaitForSeconds(1.5f);
        if (InventoryManager.CanAddItems)
        {
            this.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && InventoryManager.CanAddItems)
        {
            InventoryManager.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
