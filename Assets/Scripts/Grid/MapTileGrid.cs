using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapTileGrid : MonoBehaviour
{
    public MapTile[,] tileGrid;
    public MapTile[] grassTiles;
    public MapTile[] concreteTiles;
    public int width;
    public int height;
    public float gridSizeScale = 1; // make it bigger to spread the absolute position of the tiles out more. For bigger sprites, for example
    public GameManager gameManager;
    public int wallPosition;

    public List<Enemy> Enemies;

    // Captures input for now
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClickTile();
        }
    }

    public void addBuilding(Building b)
    {
        Vector3 buildingPosition = b.gameObject.transform.position;
        MapTile tile = GetTile(getTileCoord(buildingPosition));
        tile.buildingOnTile = b;
    }

    public Vector2 getTileCoord(Vector2 worldCoord)
    {
        worldCoord /= gridSizeScale;
        return new Vector2(Mathf.Round(worldCoord.x), Mathf.Round(worldCoord.y));
    }

    void ClickTile()
    {
        // Get the mouse position and convert it to the world position
        Vector2 rayPos = new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        MapTile clickedMapTile = GetTile(getTileCoord(rayPos));
        if (clickedMapTile == null) return;
        Debug.Log(clickedMapTile.tileNode.Value);
        // click the building if there is one
        Building b = clickedMapTile.buildingOnTile;
        if(null != b)
        {
            Debug.Log(b.buildingInfo.name);
            b.onClick();
        }
    }

    // This currently just copies the object contained in mapTile into a grid of dimensions width * height
    void Start()
    {
    }

    public void InitializeGrid()
    {
        // Instantiate the grid and populate it with defaults (for now)
        tileGrid = new MapTile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tileGrid[i, j] = Instantiate(
                      grassTiles[Random.Range(0, grassTiles.Count())],
                      new Vector3(i* gridSizeScale, j* gridSizeScale, 0.0f),
                      Quaternion.identity);
                tileGrid[i, j].name = "tile_" + i + "_" + j;
                tileGrid[i, j].mapTileGrid = this;
                tileGrid[i, j].tileNode = new TileNode(this, new Vector2Int(i, j));
                tileGrid[i, j].transform.SetParent(transform);
            }
        }
    }
    public void updateWallPosition(int position)
    {
        wallPosition = position;
        for (int i = position; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(null != tileGrid[i, j])
                {
                    GameObject.Destroy(tileGrid[i, j].gameObject);
                }
                tileGrid[i, j] = Instantiate(
                      concreteTiles[Random.Range(0, concreteTiles.Count())],
                      new Vector3(i* gridSizeScale, j* gridSizeScale, 0.0f),
                      Quaternion.identity);
                tileGrid[i, j].name = "tile_" + i + "_" + j;
                tileGrid[i, j].mapTileGrid = this;
                tileGrid[i, j].tileNode = new TileNode(this, new Vector2Int(i, j));
                tileGrid[i, j].transform.SetParent(transform);
            }
        }
    }
    // Neighbors meaning tiles that can be pathed to directly from this tile
    public Dictionary<Direction, MapTile> GetNeighboursOfTile(MapTile location)
    {
        Dictionary<Direction, MapTile> neighbours = new Dictionary<Direction, MapTile>();

        int x = location.tileNode.Value.x;
        int y = location.tileNode.Value.y;
        int mY = height;
        int mX = width;

        // Check up.
        if (y < height - 1)
        {
            int i = x;
            int j = y + 1;
            neighbours.Add(Direction.UP, tileGrid[i, j]);
        }
        // Check down
        if (y > 0)
        {
            int i = x;
            int j = y - 1;
            neighbours.Add(Direction.DOWN, tileGrid[i, j]);
        }
        // Check left
        if (x > 0)
        {
            int i = x - 1;
            int j = y;
            neighbours.Add(Direction.LEFT, tileGrid[i, j]);
        }
        // Check right
        if (x < mX - 1)
        {
            int i = x + 1;
            int j = y;
            neighbours.Add(Direction.RIGHT, tileGrid[i, j]);
        }
        return neighbours;
    }
    // Gets a tile's pathfinding node
    public TileNode GetTileNode(int x, int y)
    {
        return GetTile(x, y).tileNode;
    }

    public MapTile GetTile(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return tileGrid[x, y];
        }
        return null;
    }

    public MapTile GetTile(Vector2 position)
    {
        return GetTile((int)position.x, (int)position.y);
    }

    public TileNode GetTileNode(Vector2 position)
    {
        return GetTileNode((int)position.x, (int)position.y);
    }
}
