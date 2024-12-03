/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles Listing the Vikings and displaying that list
*/
using UnityEngine;
using TMPro;

public class VikingListPanel : MonoBehaviour
{
    public Transform vikings; // empty gameobject that contains all viking in the settlement
    public Transform listContents;
    public GameObject vikingNameButton;

    public void OpenVikingListpanel()
    {
        ListVikings();
    }
    public void CloseVikingListpanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenVikingInventory()
    {
        gameObject.SetActive(true);
    }
    public void AddVikingToList(GameObject viking)
    {
        vikingNameButton.GetComponent<TextMeshProUGUI>().text = viking.ToString(); // get the viking name this should be changed later
        Instantiate(vikingNameButton, listContents); //make the button
    }
    public void ListVikings() // only done at the start
    {
        
        foreach (var viking in vikings)
        {
            vikingNameButton.GetComponent<TextMeshProUGUI>().text = viking.ToString(); // get the viking name
            Instantiate(vikingNameButton, listContents); //make the button
        }
    }
}
