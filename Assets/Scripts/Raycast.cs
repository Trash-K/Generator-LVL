using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] float Range = 5.0f;
    void Update()
    {
       
        Ray ray = new Ray(transform.position, transform.forward);

        // Zmienna do przechowywania informacji o trafionym obiekcie
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, Range))
        {
            
            Debug.Log("Hit: " + hit.collider.gameObject.name);
        }
    }
}
