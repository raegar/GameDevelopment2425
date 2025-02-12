/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: test adding items to viking invent
*/
using UnityEngine;
using InventorySystem;
using vikingInventory;

public class TestAddItemToInvent : MonoBehaviour
{
    public Item item;
    public Item item1;
    public VikingInventory inventory;

    public void AddItem()
    {
        inventory.AddItem(item);
        inventory.ListItems();
    }
    public void Addstackable()
    { 
        inventory.AddItem(item1);
        inventory.ListItems();
    }
}
