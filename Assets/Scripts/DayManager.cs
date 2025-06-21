using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
                StartCoroutine(EndGameSequence());
            }
        }
    }

    IEnumerator EndGameSequence()
    {
       
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject ui = GameObject.Find("Canvas");

       
       
        if (ui != null) ui.SetActive(false);
        SceneManager.LoadScene("Game_Over");
        if (player != null) player.SetActive(false);

        yield return new WaitForSeconds(2f);

       
       
    }
}
