using TMPro;
using UnityEngine;

public class DayDisplay : MonoBehaviour
{
    public TextMeshProUGUI dayText;

    void Update()
    {
        if (DayManager.Instance != null)
        {
            dayText.text = $"Dzieñ {DayManager.Instance.currentDay}";
        }
    }
}
