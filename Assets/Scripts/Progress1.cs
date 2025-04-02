using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RaycastProgress : MonoBehaviour
{
    [SerializeField] float Range = 5.0f;

    [SerializeField] private float indicatorTimer = 1.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;
    [SerializeField] private Image raddialIndicatorUI = null;
    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0;
    [SerializeField] private UnityEvent myEvent = null;

    private bool shouldUpdate = false;

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward); // Tworzenie promienia z pozycji kamery w kierunku jej przodu

        RaycastHit hit;  // Zmienna do przechowywania informacji o trafionym obiekcie

        if (Input.GetKey(selectKey) && Physics.Raycast(ray, out hit, Range)) // Sprawdzenie, czy promie� trafi� w co� w zasi�gu Range jednostek
        {
            //shouldUpdate = false;
            indicatorTimer -= Time.deltaTime;
            raddialIndicatorUI.enabled = true;
            raddialIndicatorUI.fillAmount = indicatorTimer;

            Debug.Log("Hit: " + hit.collider.gameObject.name); // Wypisanie nazwy obiektu, w kt�ry trafi� promie�

            if (indicatorTimer <= 0)
            {
                indicatorTimer = maxIndicatorTimer;
                raddialIndicatorUI.fillAmount = maxIndicatorTimer;
                raddialIndicatorUI.enabled = false;
                myEvent.Invoke();
            }
        }
        else
        {
            if (shouldUpdate)
            { 
                indicatorTimer += Time.deltaTime;
                raddialIndicatorUI.fillAmount = indicatorTimer;

                if(indicatorTimer >= maxIndicatorTimer)
                {
                    indicatorTimer = maxIndicatorTimer;
                    raddialIndicatorUI.fillAmount = maxIndicatorTimer;
                    raddialIndicatorUI.enabled = false;
                    shouldUpdate = false;


                }
            }
        }

        if (Input.GetKeyUp(selectKey))
        {
            shouldUpdate = true;
        }
    }
}
