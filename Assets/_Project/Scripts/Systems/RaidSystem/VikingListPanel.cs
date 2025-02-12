/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles Listing the Vikings and displaying that list to add them to a raid
*/
using UnityEngine;
using TMPro;

public class VikingListPanel : PanelBase
{
    public Transform vikings; // empty gameobject that contains all viking in the settlement
    public Transform listContents;

    public void OpenVikingListpanel()
    {
        UIManager.Instance.OpenPanel(this);
        ListVikingNames();
    }
    public void CloseVikingListpanel()
    {
        UIManager.Instance.ClosePanel(this);
    }
    public void AddVikingToList(GameObject viking)
    {
        // create new text to add vikng to list
        TextMeshProUGUI text = new TextMeshProUGUI();
        text.text = viking.ToString(); 
        Instantiate(text, listContents); 
    }
    public void ListVikingNames() // only done at the start
    {

    }
}
