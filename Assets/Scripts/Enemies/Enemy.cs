using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyInfo info;
    private float health;

    public float wallPos;
    private bool hasReachedWall;
    public Wall wall;


    // Start is called before the first frame update
    void Start()
    {
        health = info.baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedWall)
        {
            transform.Translate(Vector3.right * info.speed * Time.deltaTime);
            if (transform.position.x >= wallPos)
            {
                hasReachedWall = true;
            }
        }
        else
        {
            atWallAction();
        }
    }

    public void atWallAction()
    {
        if (info.atWallTimer == 0)
        {
            wall.DamageWall(info.wallDamage);
            info.atWallTimer = 120;
        }
        info.atWallTimer--;
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
        GetComponent<AudioSource>().Play();
        Destroy(GetComponent<Enemy>().gameObject);
    }
}