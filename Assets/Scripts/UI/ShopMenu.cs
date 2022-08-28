using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public ShopButton shopButtonPrefab;
    public int buttonPadding;
    public List<ShopButton> shopButtons;
    public bool isOpen;
    void Start()
    {
        
    }

    public void open(ShopInventory shopInventory)
    {
        gameObject.SetActive(true);
        isOpen = true;
        shopButtons = new List<ShopButton>();
        int i = 0;
        foreach(ItemExchange itemExchange in shopInventory.inventory)
        {
            ShopButton shopButton = Instantiate(
                shopButtonPrefab,
                new Vector3(transform.position.x, transform.position.y - (i * buttonPadding), -2.0f),
                Quaternion.identity);
            shopButtons.Add(shopButton);
            shopButton.SetUp(itemExchange);
            shopButton.transform.SetParent(transform);
            i++;
        }
    }

    public void close()
    {
        gameObject.SetActive(false);
        isOpen = false;
        foreach(ShopButton button in shopButtons){
            Destroy(button.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
