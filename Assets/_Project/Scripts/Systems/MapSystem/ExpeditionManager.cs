using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionManager : MonoBehaviour
{
    public List<Expedition> expeditions;
    public List<Army> availableArmies;
    public Transform canvasTransform; // Parent for UI elements
    public GameObject expeditionSlotPrefab;

    private void Start()
    {
        SetupExpeditionSlots();
    }

    private void SetupExpeditionSlots()
    {
        for (int i = 0; i < expeditions.Count; i++)
        {
            GameObject slot = Instantiate(expeditionSlotPrefab, canvasTransform);
            ExpeditionSlot slotScript = slot.GetComponent<ExpeditionSlot>();
            slotScript.Initialize(expeditions[i], i, this);
        }
    }

    public void StartExpedition(int expeditionIndex, int armyIndex)
    {
        Expedition expedition = expeditions[expeditionIndex];
        Army army = availableArmies[armyIndex];

        if (army.totalStrength < expedition.requiredStrength)
        {
            Debug.Log("Army strength too low!");
            return;
        }

        expedition.isOngoing = true;
        expedition.endTime = DateTime.Now.AddSeconds(expedition.duration);
        StartCoroutine(UpdateExpeditionTimer(expeditionIndex));
    }

    private IEnumerator UpdateExpeditionTimer(int expeditionIndex)
    {
        Expedition expedition = expeditions[expeditionIndex];
        while (DateTime.Now < expedition.endTime)
        {
            yield return new WaitForSeconds(1);
        }
        Debug.Log($"Expedition {expedition.expeditionName} completed!");
        expedition.isOngoing = false;
    }
}
