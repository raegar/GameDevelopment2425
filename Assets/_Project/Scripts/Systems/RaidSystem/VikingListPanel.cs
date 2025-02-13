/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles Listing the Vikings and displaying that list to add them to a raid
*/
using UnityEngine;
using TMPro;

public class VikingListPanel : PanelBase
{
    public Transform listContents;
    public TextMeshProUGUI vikingName;

    public void OpenVikingListpanel()
    {
        UIManager.Instance.OpenPanel(this);
        ListVikingNames();
    }
    public void CloseVikingListpanel()
    {
        UIManager.Instance.ClosePanel(this);
    }
    public void ListVikingNames() 
    {
        //delete exsisting buttons
        foreach (GameObject go in listContents)
        {
            Destroy(go);
        }
        GameObject[] vikingList = PopulationManager.Instance.ReturnAllVikings();

        for (int i = 0; i < vikingList.Length; i++)
        {
            vikingName.text = vikingList[i].name + "+";
            Instantiate(vikingName, listContents);
        }
    }
}
