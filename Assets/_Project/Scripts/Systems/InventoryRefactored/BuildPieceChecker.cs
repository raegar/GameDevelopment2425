using Inventory2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildPieceChecker : MonoBehaviour
{
    private OnPointerDownHandler onPointerDownHandler;
    private Image image;
    // change text lable
    public TextMeshProUGUI textLabel;
    public int howMuchWoodNeeded = 20;
    // sorry dont have time to make this pretty in the inspector
    public ItemSO wood;
    public List<ItemSO> itemsRequired;
    public List<int> amountRequired;
    bool met = true;

    void Awake()
    {
        textLabel = GetComponentInChildren<TextMeshProUGUI>();
        // add list of items to lable
        textLabel.text = "";
        foreach (ItemSO item in itemsRequired)
        {
            textLabel.text += "Costs: " + amountRequired[itemsRequired.IndexOf(item)] +" "+ item.name + "\n";
        }
        onPointerDownHandler = GetComponent<OnPointerDownHandler>();
        image = GetComponent<Image>();
    }
    void OnEnable()
    {
        textLabel.text = "";
        foreach (ItemSO item in itemsRequired)
        {
            textLabel.text += "Costs: " + amountRequired[itemsRequired.IndexOf(item)] + " " + item.name + "\n";
        }
        // subscribe to observe the inventory
        SettlementInventory.Instance.OnInventoryChanged += CheckRequirementsMet;
        CheckRequirementsMet();
    }

    void OnDisable()
    {
        // subscribe to observe the inventory
        SettlementInventory.Instance.OnInventoryChanged -= CheckRequirementsMet;
        Canvas.ForceUpdateCanvases();
    }

    void CheckRequirementsMet()
    {
        foreach (ItemSO item in itemsRequired)
        {
            if (SettlementInventory.Instance.HowManyInInventory(item) < amountRequired[itemsRequired.IndexOf(item)])
            {
                met = false;
                break;
            }
            else
            {
                met = true;
            }
        }
        if (met)
        {
            onPointerDownHandler.isEnabled = true;
            image.color = Color.white;
        }
        else
        {
            onPointerDownHandler.isEnabled = false;
            image.color = Color.red;
        }
    }
}
