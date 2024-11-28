/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk & jw1519@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :A simple script to test the fuctionality of the UIManager.cs script 
 */
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
    public void OpenSettlersPanel()
    {
        UIManager.Instance.OpenPanel(settlerPanel);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        // a sloppy way to update the text on the screen - dont do this in production code
        inventoryStatusText.text = "Inventory Panel Status: " + UIManager.Instance.IsPanelOpen(inventoryPanel);
    }
}
