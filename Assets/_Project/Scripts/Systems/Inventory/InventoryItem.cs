
using UnityEngine;

namespace InventorySystem
{
    public class InventoryItem : MonoBehaviour
    {
        bool isClicked = false;
        public enum Item //declares the items. Items can be added later
        {
            Wood,
            Stone,
            iron,
            copper,

        }
        public Item itemType;
        public int amount;
        public GameObject itemInformationGO;
        public TextMesh itemInformationTextMesh;
        public string itemInformation;

        public Sprite GetSprite()
        {
            switch (itemType) 
            {
                //items that need to be in the inventory to start the game with need to be here
                default:
                case Item.Wood: return ItemAsset.instance.woodSprite;
                case Item.Stone: return ItemAsset.instance.stoneSprite;
                case Item.iron: return ItemAsset.instance.ironSprite;
                case Item.copper: return ItemAsset.instance.copperSprite;
            }
        }
        public void OnClick()
        {
            if (isClicked == false)
            {
                itemInformationGO.SetActive(true);
                
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
