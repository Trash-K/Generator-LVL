using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;         // Prefab jednego slota
    public Transform slotContainer;       // Kontener na sloty (GridLayoutGroup)

    private Dictionary<string, GameObject> activeSlots = new();

    private void OnEnable()
    {
        Invoke(nameof(DelayedUpdate), 0.1f);
        UpdateUI();
    }

    void DelayedUpdate()
    {
        if (PlayerInventory.Instance != null)
            UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);
        activeSlots.Clear();

        var inventory = PlayerInventory.Instance.GetInventory();

        foreach (var pair in inventory)
        {
            GameObject slotGO = Instantiate(slotPrefab, slotContainer);
            activeSlots[pair.Key] = slotGO;

            // Ustaw dane graficzne
            Image icon = slotGO.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI countText = slotGO.transform.Find("Count").GetComponent<TextMeshProUGUI>();

            Sprite itemIcon = ItemDatabase.Instance.GetIcon(pair.Key);
            if (itemIcon != null)
            {
                icon.sprite = itemIcon;
                icon.enabled = true;
            }
            else
            {
                Debug.LogWarning($"Brak ikony dla przedmiotu: {pair.Key}");
                icon.enabled = false;
            }

            countText.text = "x" + pair.Value;
        }
    }
}
