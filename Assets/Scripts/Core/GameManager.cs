using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemySpawner;
    public MapTileGrid grid;
    public Wall wall;
    public float wallPosition;
    public UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        wall.position = wallPosition;
        wall.BuildWall();
        enemySpawner.wallPosition = wallPosition;
        uIManager.updateWallHealth(wall.Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
