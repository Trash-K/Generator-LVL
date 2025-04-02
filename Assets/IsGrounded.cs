using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] public bool isGrounded;

    void OnCollisionEnter(Collision collision)
    {
        // Sprawdzamy, czy gracz dotyka obiektu, który ma tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Gracz dotyka ziemi
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Sprawdzamy, czy gracz przesta³ dotykaæ obiektu, który ma tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Gracz opuœci³ ziemiê
        }
    }
}
