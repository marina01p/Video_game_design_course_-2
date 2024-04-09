using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event System.Action<Collectible> OnCollectibleCollected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollectibleCollected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
