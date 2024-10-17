using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        private List<InventoryItem> itemList;
        public Inventory()
        {
            itemList = new List<InventoryItem>();
            
            AddItem(new InventoryItem { itemType = InventoryItem.Item.Wood, amount = 1 });
            AddItem(new InventoryItem { itemType = InventoryItem.Item.Stone, amount = 1 });
            AddItem(new InventoryItem { itemType = InventoryItem.Item.iron, amount = 1 });
            AddItem(new InventoryItem { itemType = InventoryItem.Item.copper, amount = 1 });
            
            Debug.Log(itemList.Count);
        }

        public void AddItem(InventoryItem item)
        {
            itemList.Add(item);
        }
        public List<InventoryItem> GetItemList()
        {
            return itemList;
        }
    }
}
