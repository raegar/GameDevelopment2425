using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Wood : Item, IStackable
    {
        public int amount = 0;
        public int maxAmount = 100;
        

        public Transform parent => gameObject.GetComponentInParent<Transform>();

        private void Update()
        {
            AddToStack(itemName, 1, 0, maxAmount );
        }

        public void AddToStack(string itemToAdd, int amountToAdd, int currentAmount, int maxAmount)
        {
            if (parent.Find(itemToAdd) != null)
            {

                if (amountToAdd + currentAmount <= maxAmount)
                {
                    //Add to stack
                    amount++;

                }
                
            }
            else
            {
                //create new object in inventory
                //AddItem and refresh inventory
                amount = 1;
            }
            

        }
    }
}
