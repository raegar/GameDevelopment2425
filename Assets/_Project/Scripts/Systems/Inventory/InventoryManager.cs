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

        private void Awake()
        {
            instance = this;
            ListItems();
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
        }
        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
        }

        public void AddItem(StackableItem item, int amountToAdd)
        {
            if (itemList.Contains(item))
            {
                if (item.currentAmount + amountToAdd <= item.maxAmount)
                {
                    item.currentAmount += amountToAdd;
                }
                else
                {
                    Debug.Log("not enough inventory space"); //the item has gone over the max amount, display in game
                }
            }
            else
            {
                itemList.Add(item);
            }
        }
        public void RemoveItem(StackableItem item, int amountToSubtract)
        {
            if (itemList.Contains(item))
            {
                item.currentAmount -= amountToSubtract;
                if (item.currentAmount == 0)
                {
                    itemList.Remove(item);
                }
                else if (item.currentAmount < 0)
                {
                    Debug.LogError("item amount is " + item.currentAmount); //make sure it doesnt go below zero
                }
            }
        }

        public void ListItems() // should be called when opening inventory
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
                    TextMeshProUGUI text = itemName.GetComponent<TextMeshProUGUI>();
                    text.text = item.itemName;
                }
            }
            foreach (StackableItem stackable in itemContainer) // doesnt work yet
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
