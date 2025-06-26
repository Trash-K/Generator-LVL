using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour
{
    public List<string> selectedItems = new List<string>(); // Nazwy wybranych przedmiotów
    public GameObject[] rewardPrefabs;
    public Transform rewardSpawnPoint;
 

    public SlotMachineUI slotUI; //  przypnij w Inspectorze

    private bool isRolling = false;
    public Transform[] reels; // np. 3 obiekty do krêcenia

    void Start()
    {
       // selectedItems.Add("Pacholek");
       // selectedItems.Add("Pacholek");
       // selectedItems.Add("Pacholek");
    }

    public void TryStartRolling()
    {
        Debug.Log(">>> Próba uruchomienia maszyny...");

        if (isRolling)
        {
            Debug.Log(" Maszyna ju¿ pracuje.");
            return;
        }

        if (selectedItems.Count != 3)
        {
            Debug.Log(" Musisz wybraæ dok³adnie 3 przedmioty!");
            return;
        }

        if (!AllInputsValid())
        {
            Debug.Log(" Nie masz wymaganych przedmiotów.");
            return;
        }

        StartCoroutine(RollCoroutine());
    }


    private bool AllInputsValid()
    {
        if (selectedItems.Count != 3)
        {
            Debug.Log($" Wybrano {selectedItems.Count} przedmioty, oczekiwano 3.");
            return false;
        }

        // Licz ile razy ka¿dy przedmiot wystêpuje w wybranych
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();
        foreach (string item in selectedItems)
        {
            if (itemCounts.ContainsKey(item))
                itemCounts[item]++;
            else
                itemCounts[item] = 1;
        }

        // SprawdŸ, czy gracz ma ich wystarczaj¹co du¿o
        foreach (var pair in itemCounts)
        {
            string itemName = pair.Key;
            int requiredAmount = pair.Value;

            if (!PlayerInventory.Instance.HasItem(itemName))
            {
                Debug.Log($" Gracz nie ma przedmiotu: {itemName}");
                return false;
            }

            // Dostêpna iloœæ w ekwipunku
            int available = PlayerInventory.Instance.GetItemCount(itemName);
            if (available < requiredAmount)
            {
                Debug.Log($" Za ma³o przedmiotów: {itemName} (potrzeba: {requiredAmount}, masz: {available})");
                return false;
            }

            Debug.Log($" {itemName} OK (potrzeba: {requiredAmount}, masz: {available})");
        }

        return true;
    }



    private IEnumerator RollCoroutine()
    {
        isRolling = true;


        // Usuwamy przedmioty z ekwipunku
        foreach (string item in selectedItems)
        {
            bool removed = PlayerInventory.Instance.RemoveItem(item);
            Debug.Log(removed
                ? $" Usuniêto przedmiot: {item}"
                : $" NIE uda³o siê usun¹æ: {item}");
        }
        slotUI.selectedText.text = " ";
        Debug.Log(">> Startujê obracanie bêbnów!");
        yield return StartCoroutine(SpinReels(3f)); // 3 sekundy krêcenia
       


        SpawnReward();
        selectedItems.Clear();
       

        isRolling = false;
        slotUI.RefreshUI();

    }
    private IEnumerator SpinReels(float duration)
    {
        float time = 0f;
        float maxSpeed = 720f; // stopnie/sekundê

        while (time < duration)
        {
            float spinSpeed = Mathf.Lerp(maxSpeed, 0, time / duration); // stopniowe zwalnianie

            foreach (Transform reel in reels)
            {
                reel.Rotate(0f, 0f, spinSpeed * Time.deltaTime, Space.Self);
            }

            time += Time.deltaTime;
            yield return null;
        }

        Debug.Log($" Obracanie zakoñczone. Finalna rotacja: {reels[0].eulerAngles}");
    }




    private void SpawnReward()
    {
        if (rewardPrefabs == null || rewardPrefabs.Length == 0)
        {
            Debug.LogError(" Brak dostêpnych prefabów nagród!");
            return;
        }

        int randomIndex = Random.Range(0, rewardPrefabs.Length);
        GameObject reward = Instantiate(rewardPrefabs[randomIndex], rewardSpawnPoint.position, Quaternion.identity);
        Debug.Log($" Wylosowano nagrodê: {reward.name}");
    }
}
