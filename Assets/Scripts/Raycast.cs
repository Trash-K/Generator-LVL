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

        // Sprawdzenie, czy promie� trafi� w co� w zasi�gu 5 jednostek
        if (Physics.Raycast(ray, out hit, Range))
        {
            // Wypisanie nazwy obiektu, w kt�ry trafi� promie�
            Debug.Log("Hit: " + hit.collider.gameObject.name);
        }
    }
}
