using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelTest : MonoBehaviour
{
    public PanelBase inventoryPanel;
    public PanelBase settlerPanel;
    public TMP_Text inventoryStatusText;

    // this is called by a button in the scene
    public void OpenInventoryPanel()
    {
       UIManager.Instance.OpenPanel(inventoryPanel);
    }
    // this is called by a button in the scene
    public void CloseInventoryPanel()
    {
        UIManager.Instance.ClosePanel(inventoryPanel);
    }
    // this is called by a button in the scene
    public void CloseAllPanels()
    {
        UIManager.Instance.CloseAllPanels();
    }

    private void Start()
    {
        UIManager.Instance.OpenPanel(settlerPanel);
    }



    // Update is called once per frame
    void Update()
    {
     //  inventoryStatusText.text = "Inventory Panel Status: " + UIManager.Instance.IsPanelOpen(inventoryPanel);
    }
}
