using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VikingListPanel : MonoBehaviour
{
    public Transform vikings;
    public Transform contents;
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
        Instantiate(vikingNameButton, contents); //make the button
    }
    public void ListVikings() // only done at the start
    {
        foreach (var viking in vikings)
        {
            vikingNameButton.GetComponent<TextMeshProUGUI>().text = viking.ToString(); // get the viking name
            Instantiate(vikingNameButton, contents); //make the button
        }
    }
}
