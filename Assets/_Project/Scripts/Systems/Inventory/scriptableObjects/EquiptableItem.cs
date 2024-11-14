/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This is a scriptable object that holds information on equiptable items such as armor
*/

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
