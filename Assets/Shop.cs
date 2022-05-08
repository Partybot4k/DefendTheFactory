using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ShopInventory inventory;
    public Building building;

    void Start()
    {
        building.onClick = openShop;
    }

    void openShop()
    {
        Debug.Log(inventory.inventory[0].resultItem);
    }

    void  Update()
    {

    }
}
