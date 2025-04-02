using UnityEngine;
using TMPro;  // U¿ywamy TextMeshPro do UI

public class PlayerInventory : MonoBehaviour
{
    private Item[] itemSlots = new Item[10]; // Tablica przechowuj¹ca przedmioty
    private int[] itemCounts = new int[10]; // Tablica przechowuj¹ca iloœæ ka¿dego przedmiotu

    public TextMeshProUGUI inventoryText;  // Pole na TextMeshPro do wyœwietlania ekwipunku

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            AddItem(item);
            other.gameObject.SetActive(false); // Dezaktywuje zamiast usuwaæ
        }
    }

    public void AddItem(Item item)
    {
        // Porównujemy na podstawie nazwy przedmiotu (unikalny identyfikator)
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null && itemSlots[i].itemName == item.itemName) // Jeœli przedmiot ju¿ istnieje
            {
                itemCounts[i]++; // Zwiêkszamy iloœæ
                return;
            }
        }

        // Jeœli przedmiot nie istnieje, dodajemy go do pierwszego wolnego miejsca
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] == null)
            {
                itemSlots[i] = item;
                itemCounts[i] = 1; // Pierwsza sztuka przedmiotu
                return;
            }
        }

        Debug.Log("Brak miejsca w ekwipunku!");
    }

    public void Update()
    {
      
            DisplayInventory(); // Wyœwietlamy zawartoœæ ekwipunku w UI
        
    }

    // Funkcja do aktualizacji tekstu w UI
    private void DisplayInventory()
    {
        string inventoryContent = "Ekwipunek gracza:";  

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null) // Tylko przedmioty w ekwipunku
            {
                inventoryContent += $"{itemSlots[i].itemName} x{itemCounts[i]}"; // Dodajemy przedmiot do tekstu
            }


        }

        inventoryText.text = inventoryContent; // Ustawiamy tekst w UI
    }
}
