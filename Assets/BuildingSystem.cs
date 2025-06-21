using UnityEngine;

public class PreviewBuildWithItem : MonoBehaviour
{   

    public string requiredItemName = "Pralka";    // Nazwa przedmiotu potrzebna do budowy
    public Material materialPreview;              // Materia³ X (pó³przezroczysty)
    public Material materialBuilt;                // Materia³ Y (po zbudowaniu)
    public float maxBuildDistance = 5f;

    public string buildID; // np. "budynek_1", "drzwi_bazy", "schody_wejœciowe"


    private bool isBuilt = false;
    private Renderer[] renderers;
    private Camera playerCamera;
    public static PreviewBuildWithItem Instance;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        playerCamera = Camera.main;

        if (GameManager.Instance != null && GameManager.Instance.IsBuilt(buildID))
        {
            isBuilt = true;
            SetMaterial(materialBuilt);
        }
        else
        {
            SetMaterial(materialPreview);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isBuilt && IsLookingAtThis())
        {
            // SprawdŸ czy gracz ma wymagany przedmiot
            if (PlayerInventory.Instance.HasItem(requiredItemName))
            {
                Build();
            }
            else
            {
                Debug.Log("Nie masz wymaganej rzeczy: " + requiredItemName);
            }
        }
    }

    bool IsLookingAtThis()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxBuildDistance))
        {
            return hit.transform == transform || hit.transform.IsChildOf(transform);
        }
        return false;
    }

    void Build()
    {
        SetMaterial(materialBuilt);
        isBuilt = true;

        PlayerInventory.Instance.RemoveItem(requiredItemName);

        Debug.Log("Zbudowano: " + requiredItemName);

        GameManager.Instance.ReportBuiltObject(buildID); //  tu przekazujemy ID
    }


    void SetMaterial(Material mat)
    {
        foreach (Renderer rend in renderers)
        {
            rend.material = mat;
        }
    }


}
