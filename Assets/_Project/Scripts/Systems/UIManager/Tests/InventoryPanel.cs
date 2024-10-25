using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : PanelBase
{
    protected override void Awake()
    {
        // Dont remove this. This is required to register the panel with the UIManager
        base.Awake();
        // do anything else that you want to do when your script wakes up
    }

    private void OnEnable()
    {
        Debug.Log("Inventory Panel is enabled");
    }

    private void OnDisable()
    {
        Debug.Log("Inventory Panel is disabled");
    }
    // do your funky stuff here
}
