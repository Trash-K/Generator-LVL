using UnityEngine;
using UnityEngine.UI;

public class ObjectTransformation : MonoBehaviour
{
    [SerializeField] private GameObject newPrefab; // Prefab, na który obiekt siê zmieni
    [SerializeField] private float holdTime = 5f; // Czas trzymania klawisza E
    [SerializeField] public Slider holdSlider; // UI Slider do wyœwietlania postêpu to takie kó³ko co siê ³aduje

    private float holdTimer = 0f;
    private bool isLookingAt = false; // Czy gracz patrzy na ten obiekt

    void Start()
    {

        // dodanie slidera (kó³ka do ³adowania UI) NIE DODAWAÆ WIÊCEJ KÓ£EK SLIDERÓW ITP BROÑ BO¯E
        Slider holdSlider = FindAnyObjectByType<Slider>();

        if (holdSlider != null)
        {
            // parametry Slidera
            holdSlider.gameObject.SetActive(false); // Ukrycie slidera
            holdSlider.maxValue = holdTime;
            holdSlider.value = 0;
        }
        else
        {
            Debug.LogError("Slider not found in the scene.");
        }

        if (holdSlider != null)
        {
            holdSlider.gameObject.SetActive(false); // Ukrycie paska progresu
            holdSlider.maxValue = holdTime;
            holdSlider.value = 0;
        }
    }

    void Update()
    {
        if (isLookingAt && Input.GetKey(KeyCode.E)) // Jeœli patrzy na obiekt i trzyma E
        {
            holdTimer += Time.deltaTime; // Zwiêksza licznik czasu
            UpdateUI();

            if (holdTimer >= holdTime) // Jeœli czas jest wiêkszy lub równy ni¿ wymagany czas
            {
                TransformObject(); // Zmienia obiekt na "Wydobyty/wykopany" 
            }
        }
        else // Jeœli nie patrzy na obiekt lub nie trzymam E
        {
            holdTimer = 0f; // Reset timer
            UpdateUI();
        }
    }

    private void TransformObject()
    {
        if (newPrefab != null)
        {
            
            Instantiate(newPrefab, transform.position, transform.rotation);
            Destroy(gameObject); //Usuwanie wkopanego/niewydobytego obiektu
        }
    }

    private void UpdateUI()
    {
        if (holdSlider != null)
        {
            if (isLookingAt && Input.GetKey(KeyCode.E))
            {
                holdSlider.gameObject.SetActive(true); // Poka¿ slider
                holdSlider.value = holdTimer; // Zaktualizuj wartoœæ slidera
            }
            else
            {
                holdSlider.gameObject.SetActive(false); // Ukryj slider
                holdSlider.value = 0; // Resetuj wartoœæ slidera
            }
        }
    }

    // Funkcja wywo³ywana przez Raycast czy patrzy
    public void SetLookingAt(bool state)
    {
        isLookingAt = state;
    }
}
