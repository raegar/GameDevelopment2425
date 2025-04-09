using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory2
{
    public class InventoryChecker : MonoBehaviour
    {
        [SerializeField] private List<ItemSO> requiredItems;
        [SerializeField] private List<int> HowManyNeeded;
        // sorry dont have time to make this pretty in the inspector
        bool met = true;
        void OnEnable()
        {
            // subscribe to observe the inventory
            SettlementInventory.Instance.OnInventoryChanged += CheckRequirementsMet;
        }

        void OnDisable()
        {
            // subscribe to observe the inventory
            SettlementInventory.Instance.OnInventoryChanged -= CheckRequirementsMet;
            Canvas.ForceUpdateCanvases();
        }

        void CheckRequirementsMet()
        {
            met = true;
            foreach (var item in requiredItems)
            {
                int needed = HowManyNeeded[requiredItems.IndexOf(item)];
                int got = SettlementInventory.Instance.HowManyInInventory(item);
                if (got < needed)
                {
                    met = false;
                    break;
                }
            }
            if (met)
            {
                Debug.Log("Requirements met");
                // do your thang here
            }
            
        }
    }
}