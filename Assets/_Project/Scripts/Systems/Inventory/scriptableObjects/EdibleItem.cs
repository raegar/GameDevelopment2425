/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This is a scriptable object that holds information on food items
*/

using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New item", menuName = "Create item/Stackable items/Food Items")]
    public class EdibleItem : StackableItem
    {
        public float hunger;
    }
}
