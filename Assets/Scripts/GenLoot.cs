using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> losujLoot = new(); // Lista mo�liwych prefab�w do losowania
    [SerializeField] private List<GameObject> lista = new(); // Lista wylosowanych loot�w

    void Start()
    {
        LootPos[] lootObjects = FindObjectsByType<LootPos>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        foreach (LootPos loot in lootObjects)
        {
            if (losujLoot.Count > 0)
            {
                // Losuje losowy prefab z listy
                GameObject randomLoot = losujLoot[Random.Range(0, losujLoot.Count)];

                // Tworzy kopie losowego lootu na pozycji loot.transform
                GameObject spawnedLoot = Instantiate(randomLoot, loot.transform.position, Quaternion.identity);

                // Dodaje do listy
                lista.Add(spawnedLoot);

                Debug.Log($"Dodano {randomLoot.name} na pozycji {loot.transform.position}");
            }
        }
        GameManager.Instance.isChangingScene = false;
    }
}
