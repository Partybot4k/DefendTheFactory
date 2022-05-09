using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public TMP_Text text;
    public Button button;

    public void SetUp(ItemExchange deal){
        text.text = deal.getShopButtonText();
    }
    // Set on shop button prefab as OnClick() event
    public void Buy(){
        Debug.Log("Thanks for your purchase!");
    }
}
