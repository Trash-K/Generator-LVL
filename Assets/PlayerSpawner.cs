using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        StartCoroutine(SpawnPlayerWithDelay());
    }

    IEnumerator SpawnPlayerWithDelay()
    {
       
        yield return null;

        if (playerPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Brakuje Prefaba lub Punktu Startowego");
            yield break;
        }

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
        else
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
