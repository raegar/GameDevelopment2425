using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class UI_Inventory : MonoBehaviour
    {
        private Inventory inventory;
        private Transform itemContainer;
        private Transform itemTemplate;

        public void SetInventory(Inventory inventory)
        {
            this.inventory = inventory;
            RefreshInventoryItems();
        }
        private void Awake()
        {
            itemContainer = transform.Find("itemContainer");
            itemTemplate = itemContainer.Find("item");
        }
        public void RefreshInventoryItems() // refresh the inventory
        {
            int x = 0;
            int y = 0;
            float itemCellSize = 30f;
            foreach (InventoryItem item in inventory.GetItemList())
            {
                RectTransform itemRectTransform = Instantiate(itemTemplate, itemContainer).GetComponent<RectTransform>();
                itemRectTransform.gameObject.SetActive(true);

                //locate the item on the grid
                itemRectTransform.anchoredPosition = new Vector2(x * itemCellSize, y * itemCellSize);
                // make sure the image is named itemImage
                Transform transform = itemRectTransform.Find("itemImage");

                if (transform != null)
                {
                    Image image = transform.GetComponent<Image>();
                    image.sprite = item.GetSprite();
                }
                
                x++;
                if (x > 4)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }
}
