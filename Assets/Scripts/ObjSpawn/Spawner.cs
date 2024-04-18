using System;
using System.Collections;
using UnityEngine;
using TMPro;

// această clasă este utilizată pentru generarea (eng. spawn) obiectelor într-o zonă preedfinită
public class Spawner : MonoBehaviour
{
    // PrefabConfig reprezintă un array de obiecte care urmează să fie generate
    [SerializeField] private PrefabConfig[] prefabConfigs;
    public PrefabConfig[] PrefabConfigs => prefabConfigs;

    // collectedPrefabsCount și OnPrefabCountChanged urmărește câte obiecte se generează în scenă
    private int[] collectedPrefabsCount;
    public event Action OnPrefabCountChanged;

    // referenceSprite este variabila pentru mărimea terenului pe care urmează să se genereze obiecte
    public SpriteRenderer referenceSprite;

    // UI
    [SerializeField] private TextMeshProUGUI[] countTexts;

    // Invetory
    [SerializeField] private Inventory inventory;

void Start()
{

    collectedPrefabsCount = new int[prefabConfigs.Length];


    for (int i = 0; i < prefabConfigs.Length; i++)
    {
        StartCoroutine(SpawnWithDelay(i));
    }

    UpdateCountTexts();
}


private void OnEnable()
{
    Collectible.OnCollectibleCollected += CollectibleCollected;
}

private void OnDisable()
{
    Collectible.OnCollectibleCollected -= CollectibleCollected;
}



    IEnumerator SpawnWithDelay(int index)
    {
        while (true)
        {
            yield return new WaitForSeconds(prefabConfigs[index].delayBetweenSpawns);
            if (GameObject.FindGameObjectsWithTag(prefabConfigs[index].prefab.tag).Length < prefabConfigs[index].maxNumberOfPrefabs)
            {
                SpawnPrefab(index);
            }
        }
    }

private void CollectibleCollected(Collectible collectible)
{

    string itemTypeID = collectible.gameObject.tag;
    Sprite itemIcon = collectible.itemIcon;

    inventory.AddItem(itemTypeID, itemIcon);

    for (int i = 0; i < prefabConfigs.Length; i++)
    {
        if (prefabConfigs[i].prefab.tag == collectible.gameObject.tag)
        {
            collectedPrefabsCount[i]++;
            UpdateCountTexts();
            OnPrefabCountChanged?.Invoke();
            break;
        }
    }

    Destroy(collectible.gameObject);
}



    private void SpawnPrefab(int index)
    {
        Vector2 spawnPosition = GetRandomPosition();
        Instantiate(prefabConfigs[index].prefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomPosition()
    {
        Bounds bounds = referenceSprite.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }

    private void UpdateCountTexts()
    {
        for (int i = 0; i < prefabConfigs.Length; i++)
        {
            if (prefabConfigs[i].textDisplay != null)
            {
                prefabConfigs[i].textDisplay.text = $"{collectedPrefabsCount[i]}";
            }
        }
    }


}
