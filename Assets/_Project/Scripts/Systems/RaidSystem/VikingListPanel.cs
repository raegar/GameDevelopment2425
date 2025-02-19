/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles Listing the Vikings and displaying that list to add them to a raid
*/
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class VikingListPanel : PanelBase
{
    public Transform listContents;
    public TextMeshProUGUI vikingName;

    GameObject[] vikings;

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
        for (int i = 0; i < listContents.childCount; i++)
        {
            Destroy(listContents.GetChild(i).gameObject);
        }
        vikings = PopulationManager.Instance.ReturnAllVikings();
        for (int i = 0; i < vikings.Length; i++)
        {
            vikingName.text = vikings[i].name + "+";
            vikingName.name = vikings[i].name;
            Instantiate(vikingName, listContents);
        }
    }
}
