using Inventory2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPieceChecker : MonoBehaviour
{
    private OnPointerDownHandler onPointerDownHandler;
    private Image image;
    public int howMuchWoodNeeded = 20;
    // sorry dont have time to make this pretty in the inspector
    public ItemSO wood;
    bool met = true;

    void Awake()
    {
        onPointerDownHandler = GetComponent<OnPointerDownHandler>();
        image = GetComponent<Image>();
    }
    void OnEnable()
    {
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
        if (SettlementInventory.Instance.HowManyInInventory(wood) > howMuchWoodNeeded)
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
