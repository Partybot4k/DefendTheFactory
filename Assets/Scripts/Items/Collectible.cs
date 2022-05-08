using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Item item;
    public int amount;
    public SpriteRenderer sprRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Apply data from item scriptable object
        sprRenderer.sprite = item.icon;
    }

    private void Update()
    {

    }

    public void attract(Vector2 dir, float weight)
    {
        transform.Translate(dir.normalized * weight * Time.deltaTime);
    }
}
