using Inventory2;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class OnPointerDownHandler : MonoBehaviour, IPointerUpHandler
{

    BuildingManager buildingManager;
    public bool isEnabled = true;
    // added by Don for the demo
    public ItemSO itemSO;
    public int buildCost = 20;
    public List<ItemSO> itemsRequired;
    public List<int> amountRequired;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isEnabled)
        {
            buildingManager.BuildMenuButtonPressed(name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buildingManager = FindAnyObjectByType<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
