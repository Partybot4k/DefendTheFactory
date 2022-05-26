using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    public int Health;
    public float position;
    public MapTileGrid grid;
    public GameObject wallSprite;
    public List<GameObject> wallBlocks;
    public UIManager UIManager;
    public float wallBlockZValue = -1;
    void Start()
    {
    }

    public void BuildWall()
    {
        // Instantiate the wall  and populate it with defaults (for now)
        wallBlocks = new List<GameObject>();
        for (int j = 0; j < grid.height; j++)
        {
            GameObject wallBlock = Instantiate(
                    wallSprite,
                    new Vector3(position * grid.gridSizeScale, j * grid.gridSizeScale, wallBlockZValue),
                    Quaternion.identity);
            wallBlock.transform.SetParent(transform);
            wallBlocks.Add(wallBlock);
        }
        grid.updateWallPosition((int)position);
    }

    public void DamageWall(int damage)
    {
        Health -= damage;
        UIManager.updateWallHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
