using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactRange = 3f;
    public LayerMask interactLayer; // warstwa dla interaktywnych obiektów
    public GameObject machineUIPanel;

    public SimpleMovement player;
    public ExampleClass cameraLook;

    private Camera cam;

    public TextMeshProUGUI interactionHintText;

    void Start()
    {
        cam = Camera.main;
        interactionHintText.alpha = 0f;
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        bool lookingAtMachine = false;

        if (Physics.Raycast(ray, out hit, interactRange, interactLayer))
        {
            if (hit.collider.GetComponent<SlotMachine>())
            {
                lookingAtMachine = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (machineUIPanel.activeSelf)
                    {
                        CloseMachineUI();
                    }
                    else
                    {
                        OpenMachineUI();
                    }
                }
            }
        }

        interactionHintText.alpha = lookingAtMachine && !machineUIPanel.activeSelf ? 1f : 0f;
    }



    void OpenMachineUI()
    {
        machineUIPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.isFrozen = true;
        cameraLook.isFrozen = true;

        FindObjectOfType<SlotMachine>()?.TryAssignUI(); // <-- dopiero tutaj
    }


    void CloseMachineUI()
    {
        machineUIPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.isFrozen = false;
        cameraLook.isFrozen = false;
    }

}
