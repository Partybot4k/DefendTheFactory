using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public TMP_Text text;

    public void SetText(ItemExchange deal){
        text.text = deal.getShopButtonText();
    }
}
