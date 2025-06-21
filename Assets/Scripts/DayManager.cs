using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    public int currentDay = 0;
    public int maxDays = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Nie usuwaj przy zmianie sceny
        }
        else
        {
            Destroy(gameObject); // Zapobiega duplikatom
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Loot_Hunt")
        {
            currentDay++;

            if (currentDay > maxDays)
            {
                // Gra si� ko�czy
                SceneManager.LoadScene("Game_Over"); // Podmie� na nazw� twojej sceny ko�cowej
            }
        }
    }
}
