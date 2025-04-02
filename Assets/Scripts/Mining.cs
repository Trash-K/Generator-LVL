using UnityEngine;
using UnityEngine.UI;

public class ObjectTransformation : MonoBehaviour
{
    [SerializeField] private GameObject newPrefab; // Prefab, na kt�ry obiekt si� zmieni
    [SerializeField] private float holdTime = 5f; // Czas trzymania klawisza E
    [SerializeField] public Slider holdSlider; // UI Slider do wy�wietlania post�pu

    private float holdTimer = 0f;
    private bool isLookingAt = false; // Czy gracz patrzy na ten obiekt

    void Start()
    {

        // Zak�adaj�c, �e w scenie masz tylko jeden obiekt Slider
        Slider holdSlider = FindAnyObjectByType<Slider>();

        if (holdSlider != null)
        {
            // Mo�esz ustawi� inne parametry Slidera, je�li chcesz
            holdSlider.gameObject.SetActive(false); // Ukrywamy Slider na pocz�tku
            holdSlider.maxValue = holdTime;
            holdSlider.value = 0;
        }
        else
        {
            Debug.LogError("Slider not found in the scene.");
        }

        if (holdSlider != null)
        {
            holdSlider.gameObject.SetActive(false); // Ukrywamy pasek na start
            holdSlider.maxValue = holdTime;
            holdSlider.value = 0;
        }
    }

    void Update()
    {
        if (isLookingAt && Input.GetKey(KeyCode.E)) // Je�li patrzymy na obiekt i trzymamy E
        {
            holdTimer += Time.deltaTime; // Zwi�kszaj licznik czasu
            UpdateUI();

            if (holdTimer >= holdTime) // Je�li czas jest wi�kszy lub r�wny ni� wymagany czas
            {
                TransformObject(); // Zmieniamy obiekt
            }
        }
        else // Je�li nie patrzymy na obiekt lub nie trzymamy E
        {
            holdTimer = 0f; // Resetujemy timer
            UpdateUI();
        }
    }

    private void TransformObject()
    {
        if (newPrefab != null)
        {
            // Tworzymy nowy prefab w tej samej pozycji i obrocie
            Instantiate(newPrefab, transform.position, transform.rotation);
            Destroy(gameObject); // Usuwamy oryginalny obiekt
        }
    }

    private void UpdateUI()
    {
        if (holdSlider != null)
        {
            if (isLookingAt && Input.GetKey(KeyCode.E))
            {
                holdSlider.gameObject.SetActive(true); // Poka� slider
                holdSlider.value = holdTimer; // Zaktualizuj warto�� slidera
            }
            else
            {
                holdSlider.gameObject.SetActive(false); // Ukryj slider
                holdSlider.value = 0; // Resetuj warto�� slidera
            }
        }
    }

    // Funkcja wywo�ywana przez Raycast w innym skrypcie
    public void SetLookingAt(bool state)
    {
        isLookingAt = state;
    }
}
