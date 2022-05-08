using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float baseHealth;
    private float health;

    public float wallPos;
    private bool hasReachedWall;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedWall)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= wallPos)
            {
                hasReachedWall = true;
            }
        } else
        {

        }
    }

    public float damage(float amount)
    {
        // Reduce enemy HP by amount
        health -= amount;

        // Call die() if at 0 health
        if (health <= 0)
        {
            die();
        }

        // Return the new health
        return health;
    }

    void die()
    {
        // Instance a collectible

        // Remove Enemy object
        Destroy(GetComponent<Enemy>());
    }
}