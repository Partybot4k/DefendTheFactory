using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Item item;
    public int amount;

    // Components
    private GameObject obj;
    public SpriteRenderer sprRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Apply data
        sprRenderer.sprite = item.icon;
    }

    private void Update()
    {
        
    }

    public void attract(Vector3 dir, float weight)
    {
        transform.Translate(dir * weight * Time.deltaTime);
        Debug.Log("Attracting");
    }

    public void pickUp()
    {
        Debug.Log($"Picking up {item.name}");
        Destroy(obj);
    }
}
