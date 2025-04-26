using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    private Item[] itemSlots = new Item[10]; // Tablica przechowuj¹ca przedmioty
    private int[] itemCounts = new int[10]; // Tablica przechowuj¹ca iloœæ ka¿dego przedmiotu

    public TextMeshProUGUI inventoryText;  // Pole na tekst do wyœwietlania ekwipunku

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
            if (itemSlots[i] != null && itemSlots[i].itemName == item.itemName) // Jeœli przedmiot ju¿ istnieje
            {
                itemCounts[i]++; // Zwiêksza iloœæ
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
                return;
            }
        }

        Debug.Log("niema miejsca");
    }

    public void Update()
    {
      
            DisplayInventory(); // Wyœwietla zawartoœæ ekwipunku na teksie w UI
        
    }

    // aktualiacja tekstu w UI
    private void DisplayInventory()
    {
        string inventoryContent = "Ekwipunek gracza:";  

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null) 
            {
                inventoryContent += $"\n{itemSlots[i].itemName} x{itemCounts[i]}"; 
            }


        }

        inventoryText.text = inventoryContent; 
    }
}
