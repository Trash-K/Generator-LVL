using UnityEngine;



public class Drop : MonoBehaviour
 
{

    [SerializeField] GameObject wybranyDrop;

     void OnDestroy()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isChangingScene)
        {
            Instantiate(wybranyDrop, transform.position, Quaternion.identity);
        }
        AudioManager.Instance.Play("Kopanie");
    }







}
