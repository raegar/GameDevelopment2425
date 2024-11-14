/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This is a scriptable object that holds information on non stackable items
*/
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu (fileName = "New item", menuName = "create item/item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite itemSprite;
    }
}
