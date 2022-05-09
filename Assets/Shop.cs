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

    void  Update()
    {

    }
}
