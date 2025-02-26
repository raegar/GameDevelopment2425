using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory2;

using TMPro;
public class InvUIItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI count;

    public void SetItem(Item item)
    {
        
        if (item.itemIcon != null)
        {
            image.sprite = item.itemIcon;
        }
        
        itemName.text = item.itemName;
        count.text = item.itemStackCount.ToString();
    }
}
