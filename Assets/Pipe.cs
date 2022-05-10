using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Start is called before the first frame update
    public Building building;
    MapTileGrid grid;
    public PipeDirection direction;
    public List<Sprite> directionToSprite; // Organzed by ordinals from PipeDirection

    public enum PipeDirection{
        HORIZONTAL,
        VERTICAL,
        CROSS,
        UP_RIGHT,
        UP_LEFT,
        DOWN_RIGHT,
        DOWN_LEFT,
        HORIZONTAL_UP,
        HORIZONTAL_DOWN,
        VERTICAL_LEFT,
        VERTICAL_RIGHT
    }
    
    public PipeItem pipeItemPrefab;

    void Start()
    {
        grid = building.grid;
        UpdateDirection(true);
    }

    void Update() {
    }

    void UpdateDirection(bool alsoUpdateNeighbors)
    {
        Vector2 gridPosition = grid.getTileCoord(new Vector2(this.transform.position.x, this.transform.position.y));
        Dictionary<Direction, MapTile> neighbors = grid.GetNeighboursOfTile(grid.GetTile(gridPosition));
        bool up = neighbors[Direction.UP] != null && neighbors[Direction.UP].buildingOnTile != null && neighbors[Direction.UP].buildingOnTile.isPipeConnectable;
        bool right = neighbors[Direction.RIGHT] != null && neighbors[Direction.RIGHT].buildingOnTile != null && neighbors[Direction.RIGHT].buildingOnTile.isPipeConnectable;
        bool down = neighbors[Direction.DOWN] != null && neighbors[Direction.DOWN].buildingOnTile != null && neighbors[Direction.DOWN].buildingOnTile.isPipeConnectable;
        bool left = neighbors[Direction.LEFT] != null && neighbors[Direction.LEFT].buildingOnTile != null && neighbors[Direction.LEFT].buildingOnTile.isPipeConnectable;
        UpdateDirection(up, down, left, right);
        if(alsoUpdateNeighbors){
            Pipe p;
            if(up)
            {
                p = neighbors[Direction.UP].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
            if(right)
            {
                p = neighbors[Direction.RIGHT].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
            if(down)
            {
                p = neighbors[Direction.DOWN].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
            if(left)
            {
                p = neighbors[Direction.LEFT].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
        }
    }

    void UpdateDirection(bool up, bool down, bool left, bool right)
    {
        if((left && right && up && down) || (!left && !right && !up && !down)){
            direction = PipeDirection.CROSS;
        }
        if((left && right && !up && !down) || (left && !right && !up && !down) || (!left && right && !up && !down)){
            direction = PipeDirection.HORIZONTAL;
        }   
        if((!left && !right && up && down)|| (!left && !right && up && !down) || (!left && !right && !up && down)){
            direction = PipeDirection.VERTICAL;
        }   
        if(!left && right && up && !down){
            direction = PipeDirection.UP_RIGHT;
        }
        if(left && !right && up && !down){
            direction = PipeDirection.UP_LEFT;
        }   
        if(!left && right && !up && down){
            direction = PipeDirection.DOWN_RIGHT;
        }   
        if(left && !right && !up && down){
            direction = PipeDirection.DOWN_LEFT;
        }
        if(left && right && up && !down){
            direction = PipeDirection.HORIZONTAL_UP;
        }   
        if(left && right && !up && down){
            direction = PipeDirection.HORIZONTAL_DOWN;
        }   
        if(left && !right && up && down){
            direction = PipeDirection.VERTICAL_LEFT;
        }
        if(!left && right && up && down){
            direction = PipeDirection.VERTICAL_RIGHT;
        }   
        building.spriteRenderer.sprite = directionToSprite[(int)direction];
    }
}
