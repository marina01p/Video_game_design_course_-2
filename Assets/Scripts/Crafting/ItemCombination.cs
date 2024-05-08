using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemCombination", menuName = "Inventory/ItemCombination")]
public class ItemCombination : ScriptableObject
{
    public Item inputItem1;
    public Item inputItem2;
    public GameObject resultPrefab;
}

