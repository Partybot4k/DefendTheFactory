using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just Item data with an amount
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


    void Start()
    {

    }


    void Update()
    {

    }
}
