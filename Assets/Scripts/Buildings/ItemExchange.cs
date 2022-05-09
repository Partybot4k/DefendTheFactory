using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemExchange", menuName = "ItemExchange")]
// Exchange rate between two items for shops and processors
public class ItemExchange : ScriptableObject
{
    public Item resultItem;
    public int resultAmount;
    public Item costItem;
    public int costAmount;

    public string getShopButtonText()
    {
        return "You get " + resultItem.name + " x " + resultAmount + " for " + costItem.name + " x " + costAmount;
    }
}
