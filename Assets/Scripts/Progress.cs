using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RaycastProgress1 : MonoBehaviour
{
    [SerializeField] float Range = 5.0f;

    [SerializeField] private float indicatorTimer = 1.0f;

    [SerializeField] private Image raddialIndicatorUI = null;
    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0;

    [SerializeField] private float maxIndicatorTimer = 1.0f;





     void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); // Tworzenie promienia z pozycji kamery w kierunku jej przodu

        RaycastHit hit;  // Zmienna do przechowywania informacji o trafionym obiekcie

        if (Input.GetKey(selectKey) && Physics.Raycast(ray, out hit, Range)) // Sprawdzenie, czy promieñ trafi³ w coœ w zasiêgu Range
        {

            indicatorTimer -= Time.deltaTime;
            raddialIndicatorUI.enabled = true;
            raddialIndicatorUI.fillAmount = indicatorTimer;

           // Debug.Log("Hit: " + hit.collider.gameObject.name); // Wypisuje co trafiono promieniem

            if (indicatorTimer <= 0 && hit.collider.CompareTag("Destroyable"))
            {
                Debug.Log("Tera mnie wypierdol!");
                indicatorTimer = 0;

                Destroy(hit.collider.gameObject);
                indicatorTimer = maxIndicatorTimer;
            }

        }


        if (!Input.GetKey(selectKey)) // czy promieñ trafi³ w coœ w zasiêgu Range
        {

            indicatorTimer += Time.deltaTime;
            raddialIndicatorUI.enabled = true;
            raddialIndicatorUI.fillAmount = indicatorTimer;


            if (indicatorTimer >= maxIndicatorTimer)
            {
                indicatorTimer = maxIndicatorTimer;
                raddialIndicatorUI.fillAmount = maxIndicatorTimer;
                raddialIndicatorUI.enabled = false;



            }

        }

    }
}
