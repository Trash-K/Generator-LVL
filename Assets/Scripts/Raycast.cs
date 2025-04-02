using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] float Range = 5.0f;
    void Update()
    {
        // Tworzenie promienia z pozycji kamery w kierunku jej przodu
        Ray ray = new Ray(transform.position, transform.forward);

        // Zmienna do przechowywania informacji o trafionym obiekcie
        RaycastHit hit;

        // Sprawdzenie, czy promieñ trafi³ w coœ w zasiêgu 5 jednostek
        if (Physics.Raycast(ray, out hit, Range))
        {
            // Wypisanie nazwy obiektu, w który trafi³ promieñ
            Debug.Log("Hit: " + hit.collider.gameObject.name);
        }
    }
}
