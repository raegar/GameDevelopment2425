using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New item", menuName = "create item/equiptable item")]
    public class EquiptableItem : Item
    {
        public int attack;
        public int defence;
        public void EquiptItem()
        {
            //update stats of viking  that want to equipt it
        }
        public void UnEquiptItem()
        {
            //update stats of viking  that want to unequipt it
        }
    }
}
