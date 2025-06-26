using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public TextMeshProUGUI inventoryText;

   
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public InventoryUI inventoryUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerInventory zachowane miêdzy scenami");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplikat PlayerInventory zniszczony");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            AddItem(item.itemName);
            other.gameObject.SetActive(false);
        }
    }

    public void AddItem(string itemName)
    {
        if (inventory.ContainsKey(itemName))
            inventory[itemName]++;
        else
            inventory[itemName] = 1;

        UpdateInventoryUI();
        inventoryUI?.UpdateUI(); // graficzne UI
    }

    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName) && inventory[itemName] > 0;
    }

    public bool RemoveItem(string itemName)
    {
        if (HasItem(itemName))
        {
            inventory[itemName]--;
            if (inventory[itemName] <= 0)
                inventory.Remove(itemName);

            UpdateInventoryUI();
            inventoryUI?.UpdateUI(); // graficzne UI
            return true;
        }

        return false;
    }

    private void UpdateInventoryUI()
    {
        if (inventoryText == null) return;

        string content = "Ekwipunek gracza:";
        foreach (var pair in inventory)
        {
            content += $"\n{pair.Key} x{pair.Value}";
        }

        inventoryText.text = content;
    }

    public int GetItemCount(string itemName)
    {
        if (inventory.ContainsKey(itemName))
            return inventory[itemName];
        return 0;
    }
    public Dictionary<string, int> GetInventory()
    {
        return new Dictionary<string, int>(inventory); // kopia, ¿eby nie da³o siê edytowaæ z zewn¹trz
    }

}
