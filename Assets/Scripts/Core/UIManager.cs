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
    // We're making it a singleton for static reference
    private UIManager() { }
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }
    void Start()
    {
        instance = this;
    }

    public void updateWallHealth(int newValue)
    {
        wallHealthValue = newValue;
        wallHealthUIComponent.text = "Wall Health: " + wallHealthValue;
    }

    public void toggleShopMenu(ShopInventory invetory)
    {
        if (!shopMenu.isOpen)
        {
            shopMenu.open(invetory);
        }
        else
        {
            shopMenu.close();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
