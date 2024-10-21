using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;

        public List<Item> itemList = new List<Item>();
        public Transform itemContainer;
        public GameObject itemTemplate;

        private void Awake()
        {
            instance = this;
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
        }

        public void ListItems()
        {
            foreach (Item item in itemList)
            {
                GameObject gameObject = Instantiate(itemTemplate, itemContainer);

                var itemImage = gameObject.transform.Find("itemImage");

                if (itemImage != null)
                {
                    Image image = itemImage.GetComponent<Image>();
                    image.sprite = item.itemSprite;
                }
                var itemName = gameObject.transform.Find("itemName");
                if (itemName != null)
                {
                    Text text = itemName.GetComponent<Text>();
                    text.text = item.itemName;

                }
            }
        }
    }
}
