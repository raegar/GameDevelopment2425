using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public interface IStackable
    {
        public Transform parent { get; }
        public void AddToStack(string itemToAdd, int amountToAdd, int currentAmount, int maxValue);
    }
}
