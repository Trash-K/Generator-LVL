using UnityEngine;
using UnityEngine.UI;

public class ObjectTransformation : MonoBehaviour
{
    [SerializeField] private GameObject newPrefab; // Prefab, na kt�ry obiekt si� zmieni
    [SerializeField] private float holdTime = 5f; // Czas trzymania klawisza E
    [SerializeField] public Slider holdSlider; // UI Slider do wy�wietlania post�pu to takie k�ko co si� �aduje

    private float holdTimer = 0f;
    private bool isLookingAt = false; // Czy gracz patrzy na ten obiekt

    void Start()
    {

        // dodanie slidera (k�ka do �adowania UI) NIE DODAWA� WI�CEJ KӣEK SLIDER�W ITP BRO� BO�E
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
        if (isLookingAt && Input.GetKey(KeyCode.E)) // Je�li patrzy na obiekt i trzyma E
        {
            holdTimer += Time.deltaTime; // Zwi�ksza licznik czasu
            UpdateUI();

            if (holdTimer >= holdTime) // Je�li czas jest wi�kszy lub r�wny ni� wymagany czas
            {
                TransformObject(); // Zmienia obiekt na "Wydobyty/wykopany" 
            }
        }
        else // Je�li nie patrzy na obiekt lub nie trzymam E
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

    // Funkcja wywo�ywana przez Raycast czy patrzy
    public void SetLookingAt(bool state)
    {
        isLookingAt = state;
    }
}
