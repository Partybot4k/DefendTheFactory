using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Shop is associated to a building and stores the inventory data, and sets the building to open the shop menu on click
public class Shop : MonoBehaviour
{
    public ShopInventory inventory;
    public Building building;
    public UIManager uiManager;

    void Start()
    {
        building.onClick = openShop;
        uiManager = UIManager.Instance;
    }

    void openShop()
    {
        Debug.Log(inventory.inventory[0].resultItem);
        uiManager.toggleShopMenu(inventory);
    }
    // This function matches a purchase to it's item and reacts accordingly
    public static void OnBuy(ItemExchange itemExchange)
    {
        switch(itemExchange.resultItem.name){
            case "Pipe":
                break;
            case "Depositor":
                ConstructionModuleFactory.InstantiateConstructionModule(itemExchange.resultItem.name);
                break;
            default:
                //int X = 5;
                break;
        }
    }

    void  Update()
    {

    }
}
