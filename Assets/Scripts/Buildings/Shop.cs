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
        uiManager.toggleShopMenu(inventory);
    }
    // This function matches a purchase to it's item and reacts accordingly
    public static void OnBuy(ItemExchange itemExchange)
    {
        if(GameManager.money < itemExchange.costAmount){
            return;
        }
        GameManager.addMoney(-1 * itemExchange.costAmount);
        switch(itemExchange.resultItem.name){
            case "Pipe":
                ConstructionModuleFactory.InstantiateConstructionModule(itemExchange.resultItem.name);
                break;
            case "Depositor":
                ConstructionModuleFactory.InstantiateConstructionModule(itemExchange.resultItem.name);
                break;
            case "AmmoFactory":
                ConstructionModuleFactory.InstantiateConstructionModule(itemExchange.resultItem.name);
                break;
            case "Turret":
                ConstructionModuleFactory.InstantiateConstructionModule(itemExchange.resultItem.name);
                break;
            default:
                break;
        }
    }

    void  Update()
    {
        // sell all items in it
        foreach (string key in building.itemNameToBuildingInventorySlot.Keys){
            if(building.itemNameToBuildingInventorySlot[key].amount > 0){
                GameManager.addMoney(building.itemNameToBuildingInventorySlot[key].amount * building.itemNameToBuildingInventorySlot[key].item.price);
                building.RemoveItemStackFromInventory(building.itemNameToBuildingInventorySlot[key]);
            }
        }
    }
}
