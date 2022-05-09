using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    public float clickDamage;
    public float clickRadius;
    public float clickCooldown;
    public float objectPickupRadius;
    public float objectPullRadius;
    public float objectPickupWeight;
    public List<PickerCollectible> pickerCollectiblesList;
    private UIManager uiManager = UIManager.Instance;

    // References

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // On click
        if (Input.GetMouseButtonDown(0))
        {
            // Play sound effect
            GetComponent<AudioSource>().Play();


            // Get all colliding objects in radius of mouse click
            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, clickRadius);

            // For each of the colliders call the damage function if it has an enemy object
            for (int i = 0; i < colliders.Length; i++)
            {
                Enemy e = colliders[i].GetComponent<Enemy>();
                e.damage(clickDamage);
            }
        }

        // Every frame
        // Get all collectable colliders
        Collider2D[] collectableColliders = Physics2D.OverlapCircleAll(mousePos, objectPullRadius);
        for (int c = 0; c < collectableColliders.Length; c++)
        {
            Collectible col = collectableColliders[c].GetComponent<Collectible>();
            Vector2 colPos = new Vector2(col.transform.position.x, col.transform.position.y);
            float distance = Vector2.Distance(mousePos, colPos);
            if (distance < objectPickupRadius)
            {
                pickUp(col);
            }

            col.attract(mousePos - colPos, objectPickupWeight);
        }
    }

    void pickUp(Collectible c)
    {
        Item itemPickedUp = c.item;
        int amtPickedUp = c.amount;
        Destroy(c.gameObject);

        // Add the collectible to the picked up objects array
        bool found = false;
        for (int i = 0; i < pickerCollectiblesList.Count; i++)
        {
            // If the item is already in the array, just increase the amount
            if (itemPickedUp == pickerCollectiblesList[i].item)
            {
                Debug.Log("adding");
                pickerCollectiblesList[i].amount += amtPickedUp;
                found = true;
                break;
            }
        }

        if (!found)
        {
            pickerCollectiblesList.Add(new PickerCollectible(itemPickedUp, amtPickedUp));
            Debug.Log("Creating new");
        }

        // Update the picker UI that follows the mouse around

    }
}
