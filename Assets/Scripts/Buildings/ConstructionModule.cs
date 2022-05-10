using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionModule : MonoBehaviour
{
    public MapTileGrid grid;
    public Building buildingPrefab;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer.sprite = buildingPrefab.buildingInfo.sprite;
    }

    void Update()
    {
        // The module follows the mouse and clicks to the grid
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = grid.getTileCoord(mousePosition);
        mousePosition *= grid.gridSizeScale;
        Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, this.transform.position.z);
        this.transform.position = new Vector3(mousePosition.x, mousePosition.y, this.transform.position.z);
        // Place the building on click
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }

    // Place the building and destory the construction module
    void PlaceBuilding()
    {
        Building building = Instantiate(
                      buildingPrefab,
                      new Vector3(transform.position.x, transform.position.y, -2.0f),
                      Quaternion.identity);
        grid.addBuilding(building);
        building.grid = grid;
        Destroy(gameObject);
    }
}
