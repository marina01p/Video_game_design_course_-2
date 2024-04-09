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


    void Start()
    {
        // se inițializează un array numit collectedPrefabsCount pentru a ține evidența prefaburilor colectate
        collectedPrefabsCount = new int[prefabConfigs.Length];

        // ciclu "for" care iterează de la 0 până la lungimea array-ului prefabConfigs
        // crește variabila i cu o unitate la fiecare iterație (i++).
        for (int i = 0; i < prefabConfigs.Length; i++)
        {
            // se lansează o coroutină (funcție care gestionează așteptări/întârzieri a unor comenzi) pentru a genera obiectele la un interval de timp decalrat în unity.
            StartCoroutine(SpawnWithDelay(i));
        }

        // se apelează metoda CollectibleCollected de fiecare dacă când este colectat un obiect
        Collectible.OnCollectibleCollected += CollectibleCollected;
        
        // Se apelează metoda UpdateCountTexts pentru a afișa numărul de obiecte colectate
        UpdateCountTexts();
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
