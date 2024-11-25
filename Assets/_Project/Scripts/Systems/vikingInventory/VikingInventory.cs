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
        public int maxItemAmount = 5;

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
        public void AddItem(Item item)
        {
            itemList.Add(item);
            if (itemList.Count == maxItemAmount)
            {
                //put items in storage if invent is full
            }
        }
        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
        }
    }
}
