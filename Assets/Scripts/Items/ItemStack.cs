using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just Item data with an amount
public class ItemStack : MonoBehaviour
{
    Item item;
    int amount;

    public ItemStack(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }


    void Start()
    {

    }


    void Update()
    {

    }
}
