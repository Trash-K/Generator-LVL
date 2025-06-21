using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;
    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Tylko jeden taki obiekt w grze
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.Instance.ChangeScene("Build_Menu"); 
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.ChangeScene("Loot_Hunt");
        }
    }
}
