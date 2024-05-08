using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : MonoBehaviour
{
    public static CombinationManager Instance { get; private set; }
    public List<ItemCombination> allCombinations;

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
        }
    }

    public ItemCombination FindCombination(Item item1, Item item2)
    {
        foreach (var combination in allCombinations)
        {
            if ((combination.inputItem1 == item1 && combination.inputItem2 == item2) ||
                (combination.inputItem1 == item2 && combination.inputItem2 == item1))
            {
                return combination;
            }
        }
        return null;
    }
}
