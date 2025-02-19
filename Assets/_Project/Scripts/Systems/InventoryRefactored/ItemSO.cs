using UnityEngine;

namespace Inventory2
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        public GameObject itemPrefab = null;
        public Sprite itemIcon = null;
        public string itemName = null;
        public itemType type = itemType.General;
        public string itemDescription = null;
        public int itemID;
        public int itemValue = 1;
        public bool isStackable = false;
        public int itemStackCount = 1;
        public int maxStackCount = 1;
        public bool isEquippable = false;
        public equipSlot slot;
        public float dropCoolDown = 20f;
        public float dropTimer = 0;
        public bool isDroppable = true;
        public bool isConsumable = false;
        public bool isQuestItem = false;
        public bool hasBeenDropped = false;
        public bool isDestroyable = true;
        public float destroytime = 0f;
        public float destroyTimer = 0f;

        /// <summary>
        /// Creates an Item from the scriptable object template
        /// </summary>
        /// <returns>the Item created</returns>
        public Item CreateItem()
        {
            Item item = new Item();
            item.itemPrefab = itemPrefab;
            item.itemName = itemName;
            item.type = type;
            item.itemDescription = itemDescription;
            item.itemIcon = itemIcon;
            item.itemID = itemID;
            item.itemValue = itemValue;
            item.isStackable = isStackable;
            item.itemStackCount = itemStackCount;
            item.maxStackCount = maxStackCount;
            item.isEquippable = isEquippable;
            item.slot = slot;
            item.dropCoolDown = dropCoolDown;
            item.dropTimer = dropTimer;
            item.isDroppable = isDroppable;
            item.isConsumable = isConsumable;
            item.isQuestItem = isQuestItem;
            item.hasBeenDropped = hasBeenDropped;
            item.isDestroyable = isDestroyable;
            item.destroytime = destroytime;
            item.destroyTimer = destroyTimer;
            return item;
        }
    }
}

