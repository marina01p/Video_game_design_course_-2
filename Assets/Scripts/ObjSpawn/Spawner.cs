using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PrefabConfig[] prefabConfigs;
    public PrefabConfig[] PrefabConfigs => prefabConfigs;

    private int[] collectedPrefabsCount;
    // public event Action OnPrefabCountChanged;

    public SpriteRenderer referenceSprite;

    void Start()
    {
        collectedPrefabsCount = new int[prefabConfigs.Length];
        for (int i = 0; i < prefabConfigs.Length; i++)
        {
            StartCoroutine(SpawnWithDelay(i));
        }
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
}
