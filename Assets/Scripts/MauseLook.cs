using UnityEngine;
using System.Collections;

// Wykonuje obrot kamery myszka z ograniczeniem rotacji pionowej.

public class ExampleClass : MonoBehaviour
{
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float minVerticalAngle = -45.0f;
    public float maxVerticalAngle = 45.0f;

    private float verticalRotation = 90.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Pobiera dane o ruchu myszy
        float h = horizontalSpeed * Input.GetAxisRaw("Mouse X");
        float v = verticalSpeed * Input.GetAxisRaw("Mouse Y");

        // Aktualizuje i ogranicza rotacje pionowa
        verticalRotation -= v;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // Zastosuj obroty
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y + h, 0);
    }
}
