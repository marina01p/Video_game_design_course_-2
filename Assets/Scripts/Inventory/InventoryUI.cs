using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    private bool isVisible = false;

    void Start()
    {
        inventoryPanel.SetActive(isVisible);
    }

    void Update()
    {
        // Debug.Log("Panel working");
        if (Input.GetKeyDown(KeyCode.E))
        {
            isVisible = !isVisible;
            inventoryPanel.SetActive(isVisible);
        }
    }
}

