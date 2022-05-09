using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public TMP_Text text;
    public Button button;
    public ItemExchange itemExchange;

    public void SetUp(ItemExchange deal){
        text.text = deal.getShopButtonText();
        itemExchange = deal;
    }
    // Set on shop button prefab as OnClick() event
    public void Buy(){
        Shop.OnBuy(itemExchange);
    }
}
