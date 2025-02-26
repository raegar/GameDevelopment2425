using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory2
{
    public class InventoryUI : MonoBehaviour
    {
        List<Item> inventoryDisplay = new List<Item>();
        public GameObject inventorySlotPrefab;
        public Transform inventoryPanel;

        void OnEnable()
        {
            // subscribe to observe the inventory
            SettlementInventory.Instance.OnInventoryChanged += UpdateUI;
            UpdateUI();
        }

        void OnDisable()
        {
            // subscribe to observe the inventory
            SettlementInventory.Instance.OnInventoryChanged -= UpdateUI;
            Canvas.ForceUpdateCanvases();
        }
        private void OnInventoryChanged()
        {
            UpdateUI();
        }
        void UpdateUI()
        {
            ClearDisplay();
            inventoryDisplay = SettlementInventory.Instance.listInventory();
            if (inventoryDisplay.Count != 0)
            {
                
                for (int i = 0; i < inventoryDisplay.Count; i++)
                {
                    GameObject tempSlot = Instantiate(inventorySlotPrefab, inventoryPanel);
                    tempSlot.GetComponent<InvUIItem>().SetItem(inventoryDisplay[i]);
                }
            }
        }
        void ClearDisplay()
        {
            foreach (Transform child in inventoryPanel)
            {
                Destroy(child.gameObject);
            }
        }
    }

}

