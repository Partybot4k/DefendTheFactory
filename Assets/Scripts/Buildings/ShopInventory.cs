using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopInventory", menuName = "ShopInventory")]
public class ShopInventory : ScriptableObject
{
    public List<ItemExchange> inventory;
}
