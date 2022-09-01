using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public Building b;
    public Item ammo;

    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    public float attackRadius;
    [SerializeField]
    private Projectile projectilePrefab;
    public Enemy targetEnemy = null;
    private float attackCounter;

    void Start()
    {
        b.inputWhiteList.Add(ammo.name);
        attackCounter = timeBetweenAttacks;
    }


	// Update is called once per frame
	void Update () {
        attackCounter -= Time.deltaTime;
        if(attackCounter <= 0f)
        {
            targetEnemy = GetClosestEnemyInRange();
            if(targetEnemy != null){
                Attack();
            }
            attackCounter = timeBetweenAttacks;
        }
	}
    public void Attack()
    {
        // Just make a projectile aimed at the target
        if(targetEnemy.isDead)
        {
            return;
        }
        if(b.itemNameToBuildingInventorySlot.ContainsKey(ammo.name) && b.itemNameToBuildingInventorySlot[ammo.name].amount > 0){
            b.removeItemFromInventory(ammo, 1);
        }
        else{
            // no ammo
            return;
        }
        Projectile newProjectile = Instantiate(projectilePrefab, new Vector3(transform.localPosition.x, transform.localPosition.y, -2.0f), Quaternion.identity);
        Vector3 targetDirection = (targetEnemy.transform.position - transform.position).normalized;
        newProjectile.direction = targetDirection;
    }

    ///Get Closest Enemy - Foreach enemy in range, get the closest enemy
    private Enemy GetClosestEnemyInRange()
    {
        Enemy closestEnemy = null;
        float smallestDistance = float.PositiveInfinity; 
        foreach(Enemy enemy in b.grid.gameManager.enemySpawner.enemies){
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if(distance < smallestDistance && distance < attackRadius)
            {
                smallestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
