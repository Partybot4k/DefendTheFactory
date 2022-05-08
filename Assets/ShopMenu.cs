using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button shopButtonPrefab;
    public int buttonPadding;
    public List<Button> shopButtons;
    void Start()
    {
        
    }

    public void open(ShopInventory shopInventory)
    {
        int i = 0;
        foreach(ItemExchange itemExchange in shopInventory.inventory)
        {
            Button shopButton = Instantiate(
                shopButtonPrefab,
                new Vector3(transform.position.x, transform.position.y * i * buttonPadding, -2.0f),
                Quaternion.identity);
                shopButtons.Add(shopButton);
                i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
