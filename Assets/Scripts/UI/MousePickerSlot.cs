using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MousePickerSlot : MonoBehaviour
{
    public Image icon;
    public int amount;
    public TMP_Text text;
    public void UpdateGraphic(ItemStack itemStack)
    {
        icon.sprite = itemStack.item.icon;
        text.text = "" + itemStack.amount;
    }
}
