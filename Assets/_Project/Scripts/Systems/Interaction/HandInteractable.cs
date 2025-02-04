using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractable : MonoBehaviour
{
    public GameObject equipableItem;

    public void Equip()
    {
        equipableItem.SetActive(true);
    }
    public void Unequip()
    {
        equipableItem.SetActive(false);
    }
}
