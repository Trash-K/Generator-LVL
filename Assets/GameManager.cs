using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool hasShownMainMenu = false;


    private int totalBuildableObjects;
    private int builtObjects = 0;
    public bool isChangingScene = false;
    
    private Dictionary<string, bool> builtObjectStates = new Dictionary<string, bool>();

    void Awake()
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

    void Start()
    {
        totalBuildableObjects = FindObjectsOfType<PreviewBuildWithItem>().Length;
        Debug.Log("Do zbudowania: " + totalBuildableObjects);
    }

    public void ChangeScene(string sceneName)
    {
        isChangingScene = true;
        SceneManager.LoadScene(sceneName);
    }


    public void ReportBuiltObject(string buildID)
    {
        if (!builtObjectStates.ContainsKey(buildID))
        {
            builtObjects++;
            Debug.Log("Zbudowane obiekty: " + builtObjects + "/" + totalBuildableObjects);
        }

        builtObjectStates[buildID] = true;

        if (builtObjects >= totalBuildableObjects)
        {
            TriggerWinState();
        }
    }

    public bool IsBuilt(string buildID)
    {
        return builtObjectStates.ContainsKey(buildID) && builtObjectStates[buildID];
    }

    void TriggerWinState()
    {
        Debug.Log(" WYGRANA! Rakieta Gotowa do Startu ");
        GameManager.Instance.ChangeScene("WinScene");
    }
}
