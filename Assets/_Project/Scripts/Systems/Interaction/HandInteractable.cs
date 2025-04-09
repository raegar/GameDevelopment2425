using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractable : MonoBehaviour
{
    public GameObject[] equipableItem;
    private int selectedItem = 0;
    public void Equip(int itemNum)
    {
        selectedItem = itemNum;
        equipableItem[itemNum].SetActive(true);
    }
    public void Unequip()
    {
        equipableItem[selectedItem].SetActive(false);
    }
}
