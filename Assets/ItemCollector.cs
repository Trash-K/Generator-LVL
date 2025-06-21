using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public static ItemCollector Instance;
    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else
        {
            Destroy(gameObject); // Tylko jeden taki obiekt w grze
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            PlayerInventory.Instance.AddItem(item.itemName);
            other.gameObject.SetActive(false);
            AudioManager.Instance.Play("Pickup");
        }
    }
}
