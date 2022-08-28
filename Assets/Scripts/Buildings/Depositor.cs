using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depositor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Just test code to test items out right now
        b.AddItemToInventory(testItem, 1);
        b.onClick = onClick;
    }

    void onClick()
    {
        b.grid.gameManager.mouseBehavior.pickerCollectiblesList.ForEach(b.AddItemStackToInventory);
        b.grid.gameManager.mouseBehavior.empty();
        b.DefaultOnClick();
    }

    public Building b;
    public Item testItem;
}
