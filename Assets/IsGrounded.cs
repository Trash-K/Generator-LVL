using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] public bool isGrounded;

    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }

    void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; 
        }
    }
}
