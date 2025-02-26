using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Inventory2;
public class _test_InventoryButtons : MonoBehaviour
{
    public ItemSO itemSO;
    private int amt = 0;

    // Start is called before the first frame update
    public void Add()
    {
        SettlementInventory.Instance.AddItem(itemSO, amt);
    }
    public void Remove()
    {
        SettlementInventory.Instance.RemoveItem(itemSO, amt);
    }
    public void SetHowMany(string howmany)
    {
        amt = int.Parse(howmany);
    }
    public void HowMany()
    {
       Debug.Log("There are :" + SettlementInventory.Instance.HowManyInInventory(itemSO));
    }
}
