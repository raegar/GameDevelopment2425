
using UnityEngine;

namespace InventorySystem
{
    public enum equipSlot
    {
        Head,
        Chest,
        Legs,
        Feet,
        Weapon,
        Shield,
        Ring,
        Neck,
        Hands
    }

    public enum itemType
    {
        Consumable,
        Coins,
        Weapon,
        Armor,
        Quest,
        General
    }

    public class Item 
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

    }
}


