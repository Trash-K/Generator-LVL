using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SlotMachineUI : MonoBehaviour
{
    public GameObject itemButtonPrefab; // prefab przycisku
    public Transform itemListContainer; // kontener na przyciski
    public TextMeshProUGUI selectedText;
    public SlotMachine slotMachine;

    void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (selectedText == null)
        {
            Debug.LogWarning("Brak przypisanego pola selectedText w SlotMachineUI!");
            return;
        }
        // Wyczyœæ stare przyciski
        foreach (Transform child in itemListContainer)
        {
            Destroy(child.gameObject);
        }

        // Pobierz przedmioty z ekwipunku gracza
        Dictionary<string, int> inventory = PlayerInventory.Instance.GetInventory();

        foreach (var item in inventory)
        {
            string itemName = item.Key;
            int count = item.Value;

            GameObject buttonGO = Instantiate(itemButtonPrefab, itemListContainer);
            TextMeshProUGUI label = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            label.text = $"{itemName} x{count}";

            Button btn = buttonGO.GetComponent<Button>();

            btn.onClick.AddListener(() => AddToMachine(itemName));
            int alreadyAdded = slotMachine.selectedItems.FindAll(i => i == itemName).Count;
            btn.interactable = slotMachine.selectedItems.Count < 3 && alreadyAdded < count;

            Image iconImage = buttonGO.transform.Find("Icon").GetComponent<Image>();
            iconImage.sprite = ItemDatabase.Instance.GetIcon(itemName);



        }

        UpdateSelectedText();

    }

    public void AddToMachine(string itemName)
    {
        Debug.Log($" Klikniêto przycisk przedmiotu: {itemName}");

        if (slotMachine.selectedItems.Count >= 3)
        {
            Debug.Log(" Maszyna przyjmuje tylko 3 przedmioty.");
            return;
        }

        int alreadyAdded = slotMachine.selectedItems.FindAll(i => i == itemName).Count;
        int playerHas = PlayerInventory.Instance.GetItemCount(itemName);

        if (alreadyAdded >= playerHas)
        {
            Debug.Log($" Nie mo¿esz dodaæ wiêcej '{itemName}' (masz tylko {playerHas})");
            return;
        }

        slotMachine.selectedItems.Add(itemName);
        Debug.Log($" Dodano do maszyny: {itemName}");

        RefreshUI();
    }



    private void UpdateSelectedText()
    {
        if (selectedText == null)
        {
            Debug.LogWarning("Brak przypisanego pola selectedText w SlotMachineUI!");
            return;
        }

        selectedText.text = "Wybrane: " + string.Join(", ", slotMachine.selectedItems);
    }


    public void ResetMachineSelection()
    {
        slotMachine.selectedItems.Clear();
        Debug.Log(" Resetowano wybrane przedmioty w maszynie.");
        RefreshUI();
    }


}
