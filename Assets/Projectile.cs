using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 direction; // must be normal
    public int damage;
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    public int lifeSpan = 100;
    void Start()
    {
        spriteRenderer.sprite = sprite;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        lifeSpan--;
        if (lifeSpan == 0)
        {
            Destroy(gameObject);
        }
    }
}
