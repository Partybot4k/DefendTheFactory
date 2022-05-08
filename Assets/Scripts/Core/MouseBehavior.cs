using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    public float clickDamage;
    public float clickRadius;
    public float clickCooldown;
    private List<Collectible> objectsPickedUp;
    public float objectPickupRadius;
    public float objectPullRadius;
    public float objectPickupWeight;

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
            Debug.Log(colliders.Length);

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
            Debug.Log(collectableColliders.Length);
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
        Destroy(c.gameObject);
    }
}
