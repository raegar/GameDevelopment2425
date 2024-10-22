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

        //test
        public Item itemToAdd;
        public StackableItem itemStack;

        private void Awake()
        {
            instance = this;
            //test
            AddItem(itemToAdd);
            AddItem(itemStack, 5);

            ListItems();

            
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
            ListItems();
        }
        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
            ListItems();
        }

        public void AddItem(StackableItem item, int amountToAdd)
        {
            if (itemList.Contains(item))
            {
                item.amount += amountToAdd;
            }
            else
            {
                itemList.Add(item);
            }
        }
        public void RemoveItem(StackableItem item, int amountToSubtract)
        {

        }

        public void ListItems()
        {
            //clean the inventory before displaying
            foreach (Transform item in itemContainer)
            {
                Destroy(item.gameObject);
            }

            //going through item list, instatiating them and setting the name and image
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
                    Text text = itemName.GetComponent<Text>(); // needs to be TMP_Text but not working
                    //text.text = item.itemName += item.amount;

                }
            }
        }
    }
}
