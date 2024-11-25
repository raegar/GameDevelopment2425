/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles the viking inventory
*/
using InventorySystem;
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
            if (itemList.Count == maxItemAmount)
            {
                //put items into storage if invent is full
            }
            else
            {
                itemList.Add(item);
            }
        }
        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
        }
    }
}
