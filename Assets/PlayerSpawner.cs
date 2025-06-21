using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;          
    public Transform spawnPoint;             

    void Start()
    {
        if (playerPrefab == null || spawnPoint == null) return;

        
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
