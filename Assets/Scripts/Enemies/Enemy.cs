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
    public bool isDead = false;

    public int dropChance;

    public Collectible drop;


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
    // Checks if hit by projectile
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.tag == "projectile")
        {
            Projectile newP = collider2D.gameObject.GetComponent<Projectile>();
            int damageAmount = newP.damage;
            Destroy(collider2D.gameObject);
            damage(damageAmount);
        }
    }
    public float damage(float amount)
    {
        // Reduce enemy HP by amount
        health -= amount;

        // Die if at 0 health
        if (health <= 0)
        {
            die();
        }
        return health;
    }

    void die()
    {
        // Remove Enemy object
        dropLoot();
        GetComponent<AudioSource>().Play();
        isDead = true;
    }

    void dropLoot(){
        if (Random.Range(0, dropChance) == 0){
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}