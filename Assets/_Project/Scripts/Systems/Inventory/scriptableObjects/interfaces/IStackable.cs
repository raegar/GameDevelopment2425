using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    public void AddToStack(int amount); 
    public void RemoveFromStack(int amount);
}
