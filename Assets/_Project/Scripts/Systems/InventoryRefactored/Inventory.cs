using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inventory2
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] public List<Item> items = new List<Item>();
        [SerializeField] private int maxInventorySize = 2000;

        // create an observer pattern to update the UI when the inventory changes 

        /// <summary>
        /// Adds an item to the inventory
        /// </summary>
        /// <param name="item"></param>
        public bool AddItem(Item item)
        {
            if (items.Count < maxInventorySize)
            {
                items.Add(item);
                Debug.Log("Added " + item.itemName + " to inventory");
                return true;
            }
            else
            {
                Debug.Log("Inventory is full");
                return false;
            }
        }

        /// <summary>
        /// Adds an item to the inventory with a specified amount
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public bool AddItem(Item item, int amount)
        {
            if (items.Count < maxInventorySize)
            {
                // we handle stackable and non-stackable items differently
                if (!item.isStackable)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        //need to handle adding past inv size AddItem(item);
                    }
                    return true;
                }
                else
                {
                    // check to see if the item is already in the inventory
                    if (!items.Contains(item))
                    {
                        items.Add(item);
                        return true;
                    }
                    else
                    {
                        return AddToStack(item, amount); ;
                    }
                }
            }
            else
            {
                Debug.Log("Inventory is full");// drop item ?
                return false;
            }
        }

        public bool AddToStack(Item item, int amount)
        {
            // loop through the items in the inventory and find an item that matches the item we want to add to the stack and check to see if it less
            // than max stack size
            return false;
        }
        public void DropItem(Item item)
        {
            // add a prefab to the base item class
            // instantiate the prefab at the player's position
        }
        public List<Item> ListItems()
        {
            return items;
        }
        /// <summary>
        /// Completely clears the inventory! Use with caution
        /// </summary>
        public void Clear()
        {
            items.Clear();
        }
        /// <summary>
        /// Checks to see if the inventory contains a specific item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Item item)
        {
            //  is each instance unique?
            return items.Contains(item);
        }

        public void TransferItem(Item item, Inventory otherInventory)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                otherInventory.AddItem(item);
            }
        }

        public void DestroyItem(Item item)
        {
            items.Remove(item);
        }
    }
}
