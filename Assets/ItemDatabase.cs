using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public Sprite icon;
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<ItemData> items; // Wype³nisz w Inspektorze

    private Dictionary<string, Sprite> iconLookup;

    void Start()
    {
        var icon = ItemDatabase.Instance.GetIcon("Pacholek");
        Debug.Log(icon != null ? " Ikona znaleziona!" : " Ikona nieznaleziona!");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            iconLookup = new Dictionary<string, Sprite>();
            foreach (var item in items)
            {
                iconLookup[item.itemName] = item.icon;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite GetIcon(string itemName)
    {
        return iconLookup.ContainsKey(itemName) ? iconLookup[itemName] : null;
    }
}
