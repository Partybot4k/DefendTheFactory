using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depositor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Just test code to test items out right now
        b.AddItemToInventory(testItem);
        b.AddItemToInventory(testItem);
        b.AddItemToInventory(testItem);
        b.AddItemToInventory(testItem);
        b.AddItemToInventory(testItem);
    }

    public Building b;
    public Item testItem;
}
