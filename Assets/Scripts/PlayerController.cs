using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded; // Sprawdzamy, czy dotykamy ziemi

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Resetujemy prêdkoœæ spadania
        }

        // Pobieranie ruchu na podstawie klawiszy WASD
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Skakanie
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        // Grawitacja
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
