using UnityEngine;
using TMPro;  // U�ywamy TextMeshPro do UI

public class PlayerInventory : MonoBehaviour
{
    private Item[] itemSlots = new Item[10]; // Tablica przechowuj�ca przedmioty
    private int[] itemCounts = new int[10]; // Tablica przechowuj�ca ilo�� ka�dego przedmiotu

    public TextMeshProUGUI inventoryText;  // Pole na TextMeshPro do wy�wietlania ekwipunku

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            AddItem(item);
            other.gameObject.SetActive(false); // Dezaktywuje zamiast usuwa�
        }
    }

    public void AddItem(Item item)
    {
        // Por�wnujemy na podstawie nazwy przedmiotu (unikalny identyfikator)
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null && itemSlots[i].itemName == item.itemName) // Je�li przedmiot ju� istnieje
            {
                itemCounts[i]++; // Zwi�kszamy ilo��
                return;
            }
        }

        // Je�li przedmiot nie istnieje, dodajemy go do pierwszego wolnego miejsca
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
      
            DisplayInventory(); // Wy�wietlamy zawarto�� ekwipunku w UI
        
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
