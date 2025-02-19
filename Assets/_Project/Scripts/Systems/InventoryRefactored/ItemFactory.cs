using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace InventorySystem
{
    public class ItemFactory : MonoBehaviour
    {
        public ItemSO[] masterListOfItems;
        private Dictionary<string, ItemSO> itemDictionary = new Dictionary<string, ItemSO>();

        void Awake()
        {
            for (int i = 0; i < masterListOfItems.Length; i++)
            {
                Debug.Log("Adding "+ i +" " + masterListOfItems[i].itemName + " to ItemFactory");
                AddItem(masterListOfItems[i]);
            }
        }

        /// <summary>
        /// Adds an ItemSO template to the ItemFactory
        /// </summary>
        /// <param name="itemSO"></param>
        /// <exception cref="System.Exception"></exception>
        public void AddItem(ItemSO itemSO)
        {

            // if the itemname does not exist in the dictionary, add it
            if (!itemDictionary.ContainsKey(itemSO.itemName)) { itemDictionary.Add(itemSO.itemName, itemSO); }
            else
            {
                // otherwise Log and throw an exception
                Debug.LogError("Item already in ItemFactory");
                throw new System.Exception("Item already in ItemFactory");
            }
        }

        /// <summary>
        /// Gets a new instance of an Item from the ItemFactory
        /// </summary>
        /// <param name="itemName">The name of the item to be created e.g. Wood, Stone etc</param>
        /// <returns>a new Item instance of the desired item</returns>
        /// <exception cref="System.Exception">exceptions for not finding the itemname in the dictionary</exception>
        public Item GetItem(string itemName)
        {
            // if the itemname exists in the dictionary, return a new instance of the item
            if (itemDictionary.ContainsKey(itemName)) { return itemDictionary[itemName].CreateItem(); }
            else
            {
                // // otherwise Log and throw an exception
                Debug.LogError("Item not found in ItemFactory");
                throw new System.Exception("Item not found in ItemFactory");
            }
        }
        /// <summary>
        /// Override of GetItem to allow for stackable items
        /// </summary>
        /// <param name="itemName">The name of the item to be created e.g. Wood, Stone etc</param>
        /// <param name="stackableNumber">The ammount to add to the stack</param>
        /// <returns>a new Item instance of the desired item with x added to the stack</returns>
        /// <exception cref="System.Exception">exceptions for not finding the itemname in the dictionary</exception>
        public Item GetItem(string itemName, int stackableNumber)
        {
            // if the itemname exists in the dictionary, return a new instance of the item
            if (itemDictionary.ContainsKey(itemName)) {
                // ensure the item is stackable
                if (itemDictionary[itemName].isStackable) {
                    // ensure the stackable number is within the max stack count
                    if (stackableNumber > itemDictionary[itemName].maxStackCount) 
                    {
                        // if not set it to the max stack count
                        stackableNumber = itemDictionary[itemName].maxStackCount; 
                    }
                    // create the new item
                    Item tmpItem = itemDictionary[itemName].CreateItem();
                    // add to the stack count
                    tmpItem.itemStackCount = stackableNumber;
                    // return the item
                    return tmpItem;
                }
            }
            // otherwise return null
            Debug.LogError("Item not found in ItemFactory");
            throw new System.Exception("Item not found in ItemFactory");
        }
    }
}

