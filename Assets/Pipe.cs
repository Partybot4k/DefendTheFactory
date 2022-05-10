using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Start is called before the first frame update
    public Building building;
    MapTileGrid grid;
    public PipeDirection direction;
    public List<Sprite> directionToSprite; // Organzed by ordinals from PipeDirectio

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
        Dictionary<int, MapTile> neighbors = grid.GetNeighboursOfTile(grid.GetTile(gridPosition));
        bool up = neighbors[0] != null && neighbors[0].buildingOnTile != null && neighbors[0].buildingOnTile.isPipeConnectable;
        bool right = neighbors[1] != null && neighbors[1].buildingOnTile != null && neighbors[1].buildingOnTile.isPipeConnectable;
        bool down = neighbors[2] != null && neighbors[2].buildingOnTile != null && neighbors[2].buildingOnTile.isPipeConnectable;
        bool left = neighbors[3] != null && neighbors[3].buildingOnTile != null && neighbors[3].buildingOnTile.isPipeConnectable;
        UpdateDirection(up, down, left, right, neighbors);
        if(alsoUpdateNeighbors){
            Pipe p;
            if(up)
            {
                p = neighbors[0].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
            if(right)
            {
                p = neighbors[1].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
            if(down)
            {
                p = neighbors[2].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
            if(left)
            {
                p = neighbors[3].buildingOnTile.gameObject.GetComponent<Pipe>();
                if(p != null)
                {
                    p.UpdateDirection(false);
                }
            }
        }
    }

    void UpdateDirection(bool up, bool down, bool left, bool right, Dictionary<int, MapTile> neighbors)
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
