using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just Item data with an amount]
[System.Serializable]
public class ItemStack
{
    public Item item;
    public int amount;

    public ItemStack(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public override string ToString()
    {
        return "${item.name}: {amount}";
    }
    // True if the stack is destroyed when the item is removed
    // Currently presupposes you never remove more than it has
    public bool lowerAmount(int i){
        amount -= i;
        return amount == 0;
    }
}
