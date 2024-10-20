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

        public string iteminformation;
        public GameObject itemInformationGO;
        //private TextMeshProUGUI itemInformationTextMesh;

        private bool isClicked = false;

        private void Start()
        {
            // itemName = gameObject.name;
            // itemNameText = GetComponentInChildren<TextMeshProUGUI>();
            //itemNameText.text = itemName; // set the item name

            itemImage = GetComponentInChildren<Image>();
            itemImage.sprite = itemSprite; // set the item sprite

        }

        public void OnClick()
        {
            if (isClicked == false)
            {
                itemInformationGO.SetActive(true);
                //itemInformationTextMesh = itemInformationGO.GetComponentInChildren<TextMeshProUGUI>();
                //itemInformationTextMesh.SetText(iteminformation);
                isClicked = true;
            }
            else
            {
                itemInformationGO.SetActive(false);
                isClicked = false;
            }
        }
    }
}
