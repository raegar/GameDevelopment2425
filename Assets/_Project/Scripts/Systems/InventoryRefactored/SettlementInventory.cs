using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PatternLibrary;

namespace Inventory2
{
    public class SettlementInventory : Singleton<SettlementInventory>
    {
        [SerializeField] public List<Item> inventoryItems = new List<Item>();
        private int runningTotal = 0;
        int tmptotal = 0;
        // an observer that lets the inventoryUI know when it has changed
        public event Action OnInventoryChanged;

        public void AddItem(ItemSO itemTemplate, int ammountToAdd = 1)
        {

            if (itemTemplate.isStackable)
            {
                Debug.Log("AddStackableItem");
                AddStackableItem(itemTemplate, ammountToAdd);
                // notify the UI the inv has changed
                OnInventoryChanged();
            }
            else
            {
                for (int i = 0; i < ammountToAdd; i++)
                {
                    inventoryItems.Add(itemTemplate.CreateItem());
                }
                // notify the UI the inv has changed
                OnInventoryChanged();
            }
            Debug.Log(inventoryItems.Count + " Items");
        }
        private void AddStackableItem(ItemSO itemTemplate, int ammountToAdd = 1)
        {
            // use a temp total to keep track of the total
            runningTotal = ammountToAdd;
            // se if ther are any stacks of this item in the inventory that are not full
            Item[] foundItems = inventoryItems.FindAll(item => (item.itemName == itemTemplate.itemName) && (item.itemStackCount < itemTemplate.maxStackCount)).ToArray();

            if (foundItems.Length > 0)
            {

                foreach (Item item in foundItems)
                {
                    // how many can this stack take?
                    int howManyToAdd = item.maxStackCount - item.itemStackCount;
                    Debug.Log("an existing stack can take: " + howManyToAdd);
                    // if we have more than this stack can take
                    if (runningTotal > howManyToAdd)
                    {
                        Debug.Log("Adding " + howManyToAdd + "to an existing stack of " + item.itemStackCount);
                        // add the max to the stack
                        item.itemStackCount += howManyToAdd;
                        // take away the ammount added from the running total
                        runningTotal -= howManyToAdd;
                    }
                    else
                    {
                        // if we have less than the stack can take
                        Debug.Log("Adding " + runningTotal + "to an existing stack of " + item.itemStackCount);
                        // add the running total to the stack
                        item.itemStackCount += runningTotal;
                        // set the running total to 0
                        runningTotal = 0;

                    }

                }
            }
            // if there were not any in or any left over create new stacks

            while (runningTotal > 0)
            {
                Debug.Log("New " + runningTotal);
                //runningTotal = ammountToAdd;
                Item newItem = itemTemplate.CreateItem();
                if (runningTotal > newItem.maxStackCount)
                {
                    newItem.itemStackCount = newItem.maxStackCount;
                    inventoryItems.Add(newItem);
                    runningTotal -= newItem.maxStackCount;
                }
                else
                {
                    newItem.itemStackCount = runningTotal;
                    inventoryItems.Add(newItem);
                    runningTotal = 0;
                }
            }

        }
        public void RemoveItem(ItemSO itemTemplate, int ammountToRemove = 1)
        {
            if (itemTemplate.isStackable)
            {
                RemoveStackableItem(itemTemplate, ammountToRemove);
                // notify the UI the inv has changed
                OnInventoryChanged();
            }
            else
            {
                for (int i = 0; i < ammountToRemove; i++)
                {
                    Item tempitem = inventoryItems.Find(item => item.itemName == itemTemplate.itemName);
                    if (tempitem != null)
                    {
                        inventoryItems.Remove(tempitem);
                    }
                }
                // notify the UI the inv has changed
                OnInventoryChanged();
            }
        }
        private void RemoveStackableItem(ItemSO itemTemplate, int ammountToRemove)
        {
            // check if we are trying to remove more than we have
            if (ammountToRemove > HowManyInInventory(itemTemplate))
            {
                throw new System.ArgumentException("You are trying to remove more than you have in the inventory! use HowManyInInventory(ItemSO itemToCheck) first to check");
            }
            if (ammountToRemove <= 0) { return; }
            runningTotal = ammountToRemove;
            //find all the items in the inventory that match the item we are trying to remove
            Item[] tempitem = inventoryItems.FindAll(item => item.itemName == itemTemplate.itemName).ToArray();
            // sort the items by stack count low to high
            Array.Sort(tempitem, (x, y) => x.itemStackCount.CompareTo(y.itemStackCount));
            // loop through the items
            foreach (Item item in tempitem)
            {
                if (runningTotal > item.itemStackCount)
                {
                    Debug.Log(runningTotal);
                    runningTotal -= item.itemStackCount;
                    inventoryItems.Remove(item);
                }
                else
                {
                    item.itemStackCount -= runningTotal;
                    if (item.itemStackCount <= 0) { inventoryItems.Remove(item); }
                    runningTotal = 0;
                }

            }
            OnInventoryChanged();
        }
        public int HowManyInInventory(ItemSO itemToCheck)
        {
            // use a temp total to keep track of the total
            tmptotal = 0;
            Item[] foundItems = inventoryItems.FindAll(item => item.itemName == itemToCheck.itemName).ToArray();
            if (itemToCheck.isStackable)
            {
                foreach (Item item in foundItems)
                {
                    // for each stack of this item add the stack count to the total
                    tmptotal += item.itemStackCount;
                }
            }
            else
            {
                tmptotal = foundItems.Length;
            }
            return tmptotal;
        }
        public List<Item> listInventory()
        {
            return inventoryItems;
        }
    }
}