using UnityEngine;

public class GravityPickup : MonoBehaviour
{
    [SerializeField] private Transform holdPosition;
    [SerializeField] private float pickupRange = 4f;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private float throwForce = 15f;

    private Rigidbody heldObject;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }

        if (Input.GetMouseButtonDown(1) && heldObject != null)
        {
            ThrowObject();
        }

        // Sprawdzamy, czy obiekt znajduje siê w zasiêgu raycasta
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            ObjectTransformation transformable = hit.collider.GetComponent<ObjectTransformation>();
            if (transformable != null)
            {
                transformable.SetLookingAt(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (heldObject != null)
        {
            MoveHeldObject();
        }
    }

    private void TryPickup()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Pickup(rb);
            }
        }
    }

    private void Pickup(Rigidbody obj)
    {
        heldObject = obj;
        heldObject.useGravity = false;
        heldObject.linearDamping = 10;
        heldObject.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void DropObject()
    {
        heldObject.useGravity = true;
        heldObject.linearDamping = 1;
        heldObject.constraints = RigidbodyConstraints.None;
        heldObject = null;
    }

    private void ThrowObject()
    {
        heldObject.useGravity = true;
        heldObject.linearDamping = 1;
        heldObject.constraints = RigidbodyConstraints.None;
        heldObject.linearVelocity = playerCamera.transform.forward * throwForce; // Rzucamy obiekt
        heldObject = null;
    }

    private void MoveHeldObject()
    {
        Vector3 targetPosition = holdPosition.position;
        heldObject.MovePosition(Vector3.Lerp(heldObject.position, targetPosition, smoothSpeed * Time.deltaTime)); // P³ynne przesuwanie
    }
}
