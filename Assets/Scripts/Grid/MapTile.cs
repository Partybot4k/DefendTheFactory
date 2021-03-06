using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public TileNode tileNode;
    public MapTileGrid mapTileGrid;
    public bool walkable;
    public Building buildingOnTile;
    public void Start()
    {
        if(null != tileNode)
        {
            tileNode.Occupied = walkable;
        }
    }
    public bool hasBuilding()
    {
        return buildingOnTile == null;
    }

}
// purely for pathfinding ignore me please but dont fuck with anything that mentions me
public class TileNode
{
    // Keep a reference to the grid so that 
    // we can find the neighbours.
    private MapTileGrid mapTileGrid;
    public Vector2Int Value { get; private set; }

    public bool Occupied;

    // construct the node with the grid and the location.
    public TileNode(MapTileGrid gridMap, Vector2Int value)
    {
        mapTileGrid = gridMap;

        // by default we set the cell to be walkable.
        Occupied = true;

        Value = value;
    }
}
