using UnityEngine;

public class UIWobble : MonoBehaviour
{
    public float rotationAmplitude = 10f;  
    public float rotationSpeed = 2f;      

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        float angleX = Mathf.Sin(Time.time * rotationSpeed) * rotationAmplitude;
        transform.localRotation = initialRotation * Quaternion.Euler(angleX, 0f, 0f);
    }
}
