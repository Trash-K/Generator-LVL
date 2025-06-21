using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuUI;
    public GameObject gameplayCanvas;

    [Header("Player root")]
    public GameObject player;

    private MonoBehaviour[] componentsToDisable;

    void Start()
    {
        if (GameManager.Instance.hasShownMainMenu)
        {
            
            if (menuUI != null) menuUI.SetActive(false);
            if (gameplayCanvas != null) gameplayCanvas.SetActive(true);
            if (player != null) player.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }

        GameManager.Instance.hasShownMainMenu = true;

        componentsToDisable = new MonoBehaviour[]
        {
            player.GetComponentInChildren<SimpleMovement>(),
            player.GetComponentInChildren<ExampleClass>(),
            
        };

        foreach (var comp in componentsToDisable)
            if (comp != null)
                comp.enabled = false;

        if (gameplayCanvas != null)
            gameplayCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        if (menuUI != null)
            menuUI.SetActive(false);

        foreach (var comp in componentsToDisable)
            if (comp != null)
                comp.enabled = true;

        if (gameplayCanvas != null)
            gameplayCanvas.SetActive(true);

        if (player != null)
            player.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}
