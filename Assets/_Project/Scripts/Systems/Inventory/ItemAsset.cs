using UnityEngine;

namespace InventorySystem
{
    public class ItemAsset : MonoBehaviour
    {
        // Sprites for items can be changed later
        public Sprite woodSprite;
        public Sprite stoneSprite;
        public Sprite ironSprite;
        public Sprite copperSprite;

        public static ItemAsset instance;
        private void Awake()
        {
            instance = this;
        }
    }
}

