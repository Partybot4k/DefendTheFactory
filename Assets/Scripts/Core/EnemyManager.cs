using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public MapTileGrid grid;
    public Enemy basicEnemy;
    public float wallPosition;
    public Wall wall;
    public List<Enemy> enemies = new List<Enemy>();
    int timer = 1;

    // Enemy types
    public EnemyInfo test;
    void Start()
    {

    }

    void SpawnDefaultEnemy()
    {
        Enemy newEnemy = Instantiate(
                      basicEnemy,
                      new Vector3(0, Random.Range(0, grid.height) * grid.gridSizeScale, -2.0f),
                      Quaternion.identity);
        newEnemy.info = test;
        newEnemy.transform.SetParent(transform);
        newEnemy.wallPos = wallPosition * grid.gridSizeScale - grid.gridSizeScale;
        newEnemy.wall = wall;
        enemies.Add(newEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        timer--;
        if (timer == 0)
        {
            SpawnDefaultEnemy();
            timer = 300;
        }
        List<Enemy> deadEnemies = enemies.FindAll((enemy => enemy.isDead));
        enemies.RemoveAll(enemy => enemy.isDead);
        deadEnemies.ForEach(enemy => Destroy(enemy.gameObject));
    }
}
