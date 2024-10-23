using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New item", menuName = "create new stackable item")]
    public class StackableItem : Item
    {
        public int currentAmount;
        public int maxAmount;
    }
}
