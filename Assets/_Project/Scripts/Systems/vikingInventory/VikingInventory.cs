using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace vikingInventory
{
    public class VikingInventory : MonoBehaviour
    {
        public List<Item> itemList = new List<Item>();
        public TextMeshProUGUI text;

        public void Awake()
        {
            ListItems();
        }
        public void ListItems()
        {
            foreach (var item in itemList)
            {
                
                Debug.Log(item.ToString());
            }
        }
    }
}
