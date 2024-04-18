using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action<Collectible> OnCollectibleCollected;
    public string itemTypeID;
    public Sprite itemIcon;

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Collectible collected by player");
        OnCollectibleCollected?.Invoke(this);
        Destroy(gameObject);
    }
}

}
