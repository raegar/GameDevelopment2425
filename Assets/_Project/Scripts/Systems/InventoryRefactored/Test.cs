using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory2 {
    public class Test : MonoBehaviour
    {
        public ItemFactory itemFactory;
        public Inventory inventory;
        public ItemSO itemSO;

        public void AddToInventory()
        {
            Item item = itemFactory.GetItem(itemSO.itemName);
            inventory.AddItem(item);
        }
    }
}
