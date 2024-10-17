using UnityEngine;

namespace InventorySystem
{
    public class InventorySetUp : MonoBehaviour
    {
        [SerializeField] UI_Inventory uiInventory;
        public void Start()
        {
            Inventory inventory = new Inventory();
            uiInventory.SetInventory(inventory);
        }
    }
}
