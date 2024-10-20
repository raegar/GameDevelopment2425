using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Wood : Item, IStackable
    {
        public Transform parent => gameObject.GetComponentInParent<Transform>();
        private void Update()
        {
            AddToStack(itemName, 1, 0, 100 );
        }

        public void AddToStack(string itemToAdd, int amountToAdd, int currentAmount, int maxValue)
        {
            if (parent.Find(itemToAdd) != null)
            {
                //Add to stack
            }
            else
            {
                //create new object in inventory
            }
            

        }
    }
}
