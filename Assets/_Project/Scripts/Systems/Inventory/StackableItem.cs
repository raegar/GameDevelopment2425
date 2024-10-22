using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New item", menuName = "create new stackable item")]
    public class StackableItem : Item, IStackable
    {
        public void AddToStack(int amountToAdd, int currentAmount, int maxAmount)
        {
            if (amountToAdd + currentAmount <= maxAmount)
            {
                InventoryManager.instance.AddItem(this, amountToAdd);
            }
            else
            {
                Debug.Log(itemName + " has reached max amount " + maxAmount);
            }
        }
        
    }
}
