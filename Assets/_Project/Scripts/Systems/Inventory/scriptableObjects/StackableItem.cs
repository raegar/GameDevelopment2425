/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This is a scriptable obaject that inherits from the item and gives extra values so it can be stacked
*/
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New item", menuName = "create item/stackable item")]
    public class StackableItem : Item, IStackable
    {
        public int currentAmount;
        public int maxAmount;

        public void AddToStack(int amount)
        {
            if (currentAmount + amount <= maxAmount)
            {
                currentAmount += amount;
            }
        }
        public void RemoveFromStack(int amount)
        {
            if (currentAmount + amount >= 0)
            {
                currentAmount -= amount;
            }
            
        }
    }
}
