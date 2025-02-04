/* Author Jess
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose: This script handles the setting and instantiating of items within the inventory UI. 
 *          It has methods to add and remove both items and stackable items from the inventory.
*/
using System.Collections.Generic;
using TMPro;
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

        public TextMeshProUGUI notificationText;

        private void Awake()
        {
            instance = this;
            ListItems();
        }
        //add and remove Items
        public void AddItem(Item item, int amountToAdd)
        {
            if (item is IStackable stackable)
            {
                if (itemList.Contains(item))
                {
                    stackable.AddToStack(amountToAdd);
                }
                else
                {
                    itemList.Add(item);
                }
            }
            else
            {
                itemList.Add(item);
            }
        }
        public void RemoveItem(Item item, int amountToSubtract)
        {
            if (item is IStackable stackable)
            {
                stackable.AddToStack(amountToSubtract);
            }
            else
            {
                itemList.Remove(item);
            }
        }
        public void ListItems() // should be called when opening inventory
        {
            //clean the inventory before displaying
            foreach (Transform item in itemContainer)
            {
                Destroy(item.gameObject);
            }

            //going through item list to instatiating them 
            foreach (Item item in itemList)
            {
                GameObject gameObject = Instantiate(itemTemplate, itemContainer);

                //setting itemImage
                var itemImage = gameObject.transform.Find("itemImage");
                if (itemImage != null)
                {
                    Image image = itemImage.GetComponent<Image>();
                    image.sprite = item.itemSprite;
                }

                //setting itemName
                var itemName = gameObject.transform.Find("itemName");
                if (itemName != null)
                {
                    TextMeshProUGUI text = itemName.GetComponent<TextMeshProUGUI>();
                    text.text = item.itemName;
                }

                //setting itemAmount if its stackable
                if (item is StackableItem stackable)
                {
                    var itemAmount = gameObject.transform.Find("amountText");
                    if (itemAmount != null)
                    {
                        TextMeshProUGUI amountText = itemAmount.GetComponent<TextMeshProUGUI>();
                        amountText.text = stackable.currentAmount.ToString() + "/" + stackable.maxAmount.ToString();
                    }
                }
            }
        }
    }
}
