//using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace InventorySystem
{
    public class Item : MonoBehaviour
    {
        //item details
        public string itemName;
        //public TextMeshProUGUI itemNameText;

        public Sprite itemSprite;
        public Image itemImage;
        public int amount;

        private void Start()
        {
            // itemName = gameObject.name;
            // itemNameText = GetComponentInChildren<TextMeshProUGUI>();
            //itemNameText.text = itemName; // set the item name

            itemImage = GetComponentInChildren<Image>();
            itemImage.sprite = itemSprite; // set the item sprite

        }
    }
}
