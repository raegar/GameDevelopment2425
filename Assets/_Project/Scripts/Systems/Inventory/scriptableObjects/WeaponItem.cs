using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New item", menuName = "create item/weapon")]
    public class WeaponItem : Item
    {
        public int damage = 10;
    }
}
