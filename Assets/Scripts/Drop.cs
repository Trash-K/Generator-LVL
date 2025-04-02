using UnityEngine;



public class Drop : MonoBehaviour
 
{

    [SerializeField] GameObject wybranyDrop;

     void OnDestroy()
    { 
        Instantiate(wybranyDrop, transform.position, Quaternion.identity);
    }







}
