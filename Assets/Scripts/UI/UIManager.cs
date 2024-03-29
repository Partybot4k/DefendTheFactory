using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text wallHealthUIComponent;
    public Text moneyUIComponent;
    public ShopMenu shopMenu;
    public int wallHealthValue;
    public int moneyValue;
    // We're making it a singleton for static reference
    private UIManager() { }
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                //instance = this.gameObject.AddComponent<UIManager>();
            }
            return instance;
        }
    }
    public MousePickerUI mousePickerUI;
    void Start()
    {
        instance = this;
    }

    public void updateWallHealth(int newValue)
    {
        wallHealthValue = newValue;
        wallHealthUIComponent.text = "Wall Health: " + wallHealthValue;
    }

    public void updateMoney(int newValue)
    {
        moneyValue = newValue;
        moneyUIComponent.text = "Money: " + moneyValue;
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

    public void UpdateMousePickerUI(List<ItemStack> item){
        mousePickerUI.UpdatePickerUI(item);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
