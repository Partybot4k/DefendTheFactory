using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text wallHealthUIComponent;
    public ShopMenu shopMenu;
    public int wallHealthValue;
    void Start()
    {
    }

    public void updateWallHealth(int newValue)
    {
        wallHealthValue = newValue;
        wallHealthUIComponent.text = "Wall Health: " + wallHealthValue;
    }

    public void openShopMenu(ShopInventory invetory)
    {
        shopMenu.open(invetory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
