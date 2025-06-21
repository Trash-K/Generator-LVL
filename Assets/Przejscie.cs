using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string sceneToLoad = "Loot_Hunt";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            GameManager.Instance.ChangeScene(sceneToLoad);
        }
    }
}
