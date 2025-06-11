using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    // Singleton
    public static PlayerInventory Instance;

    private Item[] itemSlots = new Item[10]; // Tablica przechowuj¹ca przedmioty
    private int[] itemCounts = new int[10];  // Tablica przechowuj¹ca iloœæ ka¿dego przedmiotu

    public TextMeshProUGUI inventoryText;  // Pole na tekst do wyœwietlania ekwipunku

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
            AddItem(item);
            other.gameObject.SetActive(false); // Dezaktywuje gameObject
        }
    }

    public void AddItem(Item item)
    {
        // Porównuje na podstawie nazwy przedmiotu
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null && itemSlots[i].itemName == item.itemName)
            {
                itemCounts[i]++;
                UpdateInventoryUI();
                return;
            }
        }

        // Jeœli przedmiot nie istnieje, dodaje go do pierwszego wolnego miejsca
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] == null)
            {
                itemSlots[i] = item;
                itemCounts[i] = 1;
                UpdateInventoryUI();
                return;
            }
        }

        Debug.Log("Brak miejsca w ekwipunku!");
    }

    

    private void UpdateInventoryUI()
    {
        string inventoryContent = "Ekwipunek gracza:";

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null)
            {
                inventoryContent += $"\n{itemSlots[i].itemName} x{itemCounts[i]}";
            }
        }

        if (inventoryText != null)
            inventoryText.text = inventoryContent;
    }
}
