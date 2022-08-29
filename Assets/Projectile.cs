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
    void Start()
    {
        spriteRenderer.sprite = sprite;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
