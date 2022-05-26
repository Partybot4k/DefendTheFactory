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
    public ConstructionModuleFactory cmFactory;
    public MouseBehavior mouseBehavior;
    // Start is called before the first frame update
    void Start()
    {
        grid.InitializeGrid();
        wall.position = wallPosition;
        wall.BuildWall();
        enemySpawner.wallPosition = wallPosition;
        uIManager.updateWallHealth(wall.Health);
        ConstructionModuleFactory.grid = grid;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
