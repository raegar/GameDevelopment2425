using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu (fileName = "New item", menuName = "create new item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public int amount;
        public Sprite itemSprite;
    }
}
