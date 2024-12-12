using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    public StackableItem StackableItem;
    public Item item;
    public StackableItem StackableItem1;

    public void AddItem()
    {
        InventoryManager.instance.AddItem(StackableItem, StackableItem.currentAmount);
        InventoryManager.instance.ListItems();
    }
}
