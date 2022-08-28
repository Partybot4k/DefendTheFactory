using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refinery : MonoBehaviour
{
    public ItemExchange itemExchange;
    void Start()
    {
        //whitelist the items it needs
        b.inputWhiteList.Add(itemExchange.costItem.name);
        b.inputWhiteList.Add(itemExchange.resultItem.name);
        b.AddItemToInventory(testItem, 1);
        b.onClick = onClick;
    }
    //just logging for now
    void onClick()
    {
        Debug.Log(itemExchange.getShopButtonText());
    }

    void Update() {
        if(b.itemNameToBuildingInventorySlot[itemExchange.costItem.name] != null && b.itemNameToBuildingInventorySlot[itemExchange.costItem.name].amount > itemExchange.costAmount){
            b.removeItemFromInventory(itemExchange.costItem, itemExchange.costAmount);
            b.AddItemToInventory(itemExchange.resultItem, itemExchange.resultAmount);
        }
    }

    public Building b;
    public Item testItem;
}
