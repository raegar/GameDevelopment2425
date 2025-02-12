using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class ExpeditionSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Expedition expedition;
    public TMP_Text expeditionNameText;
    public GameObject popupPanel; // Reference to popup UI
    public TMP_Text lootText;
    public TMP_Text strengthText;
    public TMP_Text difficultyText;

    private ExpeditionManager expeditionManager;
    private int expeditionIndex;

    public void Initialize(Expedition expedition, int index, ExpeditionManager manager)
    {
        this.expedition = expedition;
        this.expeditionIndex = index;
        this.expeditionManager = manager;

        expeditionNameText.text = expedition.expeditionName;
        UpdatePopupInfo();
        popupPanel.SetActive(false);
    }

    private void UpdatePopupInfo()
    {
        lootText.text = $"Loot: {expedition.reward.rewardType} x{expedition.reward.amount}";
        strengthText.text = $"Required Strength: {expedition.requiredStrength}";
        difficultyText.text = GetDifficultyText();
    }

    private string GetDifficultyText()
    {
        if (expedition.requiredStrength < 50) return "Easy";
        if (expedition.requiredStrength < 100) return "Medium";
        return "Hard";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        popupPanel.SetActive(true);
        popupPanel.transform.position = Input.mousePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popupPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        expeditionManager.StartExpedition(expeditionIndex, 0); // Update with army selection logic
    }
}
