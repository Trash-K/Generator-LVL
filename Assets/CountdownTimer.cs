using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 60f;
    public TextMeshProUGUI timerText;

    private float timeRemaining;
    private bool timerIsRunning = false;
    private static CountdownTimer instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        timeRemaining = startTime;
        TryFindTimerText();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            timeRemaining = Mathf.Max(0, timeRemaining);

            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                timerIsRunning = false;
                GameManager.Instance.ChangeScene("Build_Menu");
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            timeRemaining -= 10f;
            timeRemaining = Mathf.Max(0, timeRemaining);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryFindTimerText();

        if (scene.name == "Loot_Hunt")
        {
            timeRemaining = startTime;
            timerIsRunning = true;
        }
        else if (scene.name == "Build_Menu")
        {
            timerIsRunning = false;
        }
    }

    void TryFindTimerText()
    {
        GameObject foundText = GameObject.Find("CountdownText");
        if (foundText != null)
        {
            timerText = foundText.GetComponent<TextMeshProUGUI>();
            UpdateTimerUI(); 
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);
            int minutes = seconds / 60;
            seconds %= 60;
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
