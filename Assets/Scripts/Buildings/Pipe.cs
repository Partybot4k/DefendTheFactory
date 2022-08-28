using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Start is called before the first frame update
    public Building building;
    MapTileGrid grid;
    public PipeOrientation pipeOrientation;
    public List<Sprite> pipeOrientationToSprite; // Organzed by ordinals from PipeOrientation
    public List<Direction> inputDirections = new List<Direction>(); // Directions from which the pipe can receive input
    public Dictionary<Direction,List<PipeItem>> itemsInPipe = new Dictionary<Direction, List<PipeItem>>(); // Stored according to the direction they are moving
    List<PipeItem> pipeItemsInPipe = new List<PipeItem>();
    public PipeItem pipeItemPrefab;
    public float pipeSpeed = 0.1f;

    public enum PipeOrientation{
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
        building.ReceiveADeposit_del = ReceiveADeposit;
        building.CanAcceptDeposit_del = CanAcceptDeposit;
        itemsInPipe.Add(Direction.UP, new List<PipeItem>());
        itemsInPipe.Add(Direction.DOWN, new List<PipeItem>());
        itemsInPipe.Add(Direction.LEFT, new List<PipeItem>());
        itemsInPipe.Add(Direction.RIGHT, new List<PipeItem>());
    }

    void Update()
    {
        MoveItems();
    }

    void MoveItems()
    {
        // Move pipe items in an intelligent direction
        for (int i = itemsInPipe[Direction.UP].Count - 1; i >= 0; i--)
        {
            MovePipeItem(itemsInPipe[Direction.UP][i], Direction.UP);
        }
        for (int i = itemsInPipe[Direction.DOWN].Count - 1; i >= 0; i--)
        {
            MovePipeItem(itemsInPipe[Direction.DOWN][i], Direction.DOWN);
        }
          for (int i = itemsInPipe[Direction.LEFT].Count - 1; i >= 0; i--)
        {
            MovePipeItem(itemsInPipe[Direction.LEFT][i], Direction.LEFT);
        }
        for (int i = itemsInPipe[Direction.RIGHT].Count - 1; i >= 0; i--)
        {
            MovePipeItem(itemsInPipe[Direction.RIGHT][i], Direction.RIGHT);
        }
    }

    void MovePipeItem(PipeItem pipeItem, Direction directionItemIsInitiallyTraveling)
    {
        Vector3 initialPosition = new Vector3(pipeItem.transform.localPosition.x, pipeItem.transform.localPosition.y, pipeItem.transform.position.z);
        pipeItem.transform.Translate(pipeItem.directionVector * pipeItem.speed * Time.deltaTime);
        Vector3 newPosition = new Vector3(pipeItem.transform.localPosition.x, pipeItem.transform.localPosition.y, pipeItem.transform.position.z);
        switch(directionItemIsInitiallyTraveling){
            case Direction.UP:
                if(initialPosition.y < 0.0f && newPosition.y >= 0.0f){
                    switchMovementOfPipeItemInMiddle(pipeItem, directionItemIsInitiallyTraveling);
                }
                else if(initialPosition.y >= grid.gridSizeScale / 2){
                    DepositOrFlipMovement(pipeItem, directionItemIsInitiallyTraveling);
                }
                break;
            case Direction.DOWN:
                if(initialPosition.y > 0.0f && newPosition.y <= 0.0f){
                    switchMovementOfPipeItemInMiddle(pipeItem, directionItemIsInitiallyTraveling);
                }
                else if(initialPosition.y <= grid.gridSizeScale / -2){
                    DepositOrFlipMovement(pipeItem, directionItemIsInitiallyTraveling);
                }
                break;
            case Direction.LEFT:
                if(initialPosition.x > 0.0f && newPosition.x <= 0.0f){
                    switchMovementOfPipeItemInMiddle(pipeItem, directionItemIsInitiallyTraveling);
                }
                else if(initialPosition.x <= grid.gridSizeScale / -2){
                    DepositOrFlipMovement(pipeItem, directionItemIsInitiallyTraveling);
                }
                break;
            case Direction.RIGHT:
                if(initialPosition.x < 0.0f && newPosition.x >= 0.0f){
                    switchMovementOfPipeItemInMiddle(pipeItem, directionItemIsInitiallyTraveling);
                }
                else if(initialPosition.x >= grid.gridSizeScale / 2){
                    DepositOrFlipMovement(pipeItem, directionItemIsInitiallyTraveling);
                }
                break;
        }
    }

    void switchMovementOfPipeItemInMiddle(PipeItem pipeItem, Direction directionItemIsInitiallyTraveling)
    {
        //pipeItem.transform.localPosition = new Vector3(0.0f, 0.0f, pipeItem.transform.localPosition.z);
        Direction newDirection = directionItemIsInitiallyTraveling;
        switch(directionItemIsInitiallyTraveling){
            case Direction.LEFT:
                switch(pipeOrientation)
                {
                    case PipeOrientation.UP_RIGHT:
                        newDirection = Direction.UP;
                        break;
                    case PipeOrientation.DOWN_RIGHT:
                        newDirection = Direction.DOWN;
                        break;
                    case PipeOrientation.VERTICAL_RIGHT:
                        newDirection = Direction.DOWN; // Arbitrarily picking down right now, could be up
                        break;
                }
                break;
            case Direction.RIGHT:
                switch(pipeOrientation)
                {
                    case PipeOrientation.UP_LEFT:
                        newDirection = Direction.UP;
                        break;
                    case PipeOrientation.DOWN_LEFT:
                        newDirection = Direction.DOWN;
                        break;
                    case PipeOrientation.VERTICAL_LEFT:case PipeOrientation.VERTICAL_RIGHT:
                        newDirection = Direction.DOWN; // Arbitrarily picking down right now, could be up
                        break;
                    }
                break;
            case Direction.UP:
                switch(pipeOrientation)
                {
                    case PipeOrientation.DOWN_RIGHT:
                        newDirection = Direction.RIGHT;
                        break;
                    case PipeOrientation.DOWN_LEFT:
                        newDirection = Direction.LEFT;
                        break;
                    case PipeOrientation.HORIZONTAL_DOWN:
                        newDirection = Direction.LEFT;
                        break;
                }
                break;
            case Direction.DOWN:
                switch(pipeOrientation)
                {
                    case PipeOrientation.UP_RIGHT:
                        newDirection = Direction.RIGHT;
                        break;
                    case PipeOrientation.UP_LEFT:
                        newDirection = Direction.LEFT;
                        break;
                    case PipeOrientation.HORIZONTAL_UP:
                        newDirection = Direction.LEFT;
                        break;
                }
            break;
        }
        pipeItem.setDirection(newDirection);
        itemsInPipe[directionItemIsInitiallyTraveling].Remove(pipeItem);
        itemsInPipe[newDirection].Add(pipeItem);
    }
    void DepositOrFlipMovement(PipeItem pipeItem, Direction directionItemIsInitiallyTraveling){
        if(building.TryDepositOnNeighbor(pipeItem.item, directionItemIsInitiallyTraveling)){
            itemsInPipe[directionItemIsInitiallyTraveling].Remove(pipeItem);
            Destroy(pipeItem.gameObject);
        }
        else
        {
            Direction newDirection = Building.Flip(directionItemIsInitiallyTraveling);
            pipeItem.setDirection(newDirection);
            itemsInPipe[directionItemIsInitiallyTraveling].Remove(pipeItem);
            itemsInPipe[newDirection].Add(pipeItem);
        }
    }
    public void ReceiveADeposit(Direction itemSourceDirection, Item item)
    {
        float gridSizeScale = grid.gridSizeScale;
        itemSourceDirection = Building.Flip(itemSourceDirection);
        Vector2 initialPositionRelativetoPipe = new Vector2();
        switch(itemSourceDirection){
            case Direction.UP:
                initialPositionRelativetoPipe.x = 0;
                initialPositionRelativetoPipe.y = gridSizeScale / -2;
                break;
            case Direction.DOWN:
                initialPositionRelativetoPipe.x = 0;
                initialPositionRelativetoPipe.y = gridSizeScale / 2;
                break;
            case Direction.LEFT:
                initialPositionRelativetoPipe.x = gridSizeScale / 2;
                initialPositionRelativetoPipe.y = 0;
                break;
            case Direction.RIGHT:
                initialPositionRelativetoPipe.x = gridSizeScale / -2;
                initialPositionRelativetoPipe.y = 0;
                break;
            default:
                break;
        }
        PipeItem newPipeItem = Instantiate(
                      pipeItemPrefab,
                      new Vector3(initialPositionRelativetoPipe.x, initialPositionRelativetoPipe.y, -3.0f),
                      Quaternion.identity);
        newPipeItem.item = item;
        newPipeItem.transform.SetParent(transform);
        newPipeItem.transform.localPosition = newPipeItem.transform.position;
        newPipeItem.speed = 0.4f;
        newPipeItem.setDirection(itemSourceDirection);
        itemsInPipe[itemSourceDirection].Add(newPipeItem);
        pipeItemsInPipe.Add(newPipeItem);
    }

    public bool CanAcceptDeposit(Direction itemSourceDirection, Item item)
    {
        return inputDirections.Contains(itemSourceDirection);
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
        inputDirections.Clear();
        if((left && right && up && down) || (!left && !right && !up && !down)){
            pipeOrientation = PipeOrientation.CROSS;
            inputDirections.Add(Direction.UP);
            inputDirections.Add(Direction.DOWN);
            inputDirections.Add(Direction.LEFT);
            inputDirections.Add(Direction.RIGHT);
        }
        if((left && right && !up && !down) || (left && !right && !up && !down) || (!left && right && !up && !down)){
            pipeOrientation = PipeOrientation.HORIZONTAL;
            inputDirections.Add(Direction.LEFT);
            inputDirections.Add(Direction.RIGHT);
        }   
        if((!left && !right && up && down)|| (!left && !right && up && !down) || (!left && !right && !up && down)){
            pipeOrientation = PipeOrientation.VERTICAL;
            inputDirections.Add(Direction.UP);
            inputDirections.Add(Direction.DOWN);
        }   
        if(!left && right && up && !down){
            pipeOrientation = PipeOrientation.UP_RIGHT;
            inputDirections.Add(Direction.UP);
            inputDirections.Add(Direction.RIGHT);
        }
        if(left && !right && up && !down){
            pipeOrientation = PipeOrientation.UP_LEFT;
            inputDirections.Add(Direction.UP);
            inputDirections.Add(Direction.LEFT);
        }   
        if(!left && right && !up && down){
            pipeOrientation = PipeOrientation.DOWN_RIGHT;
            inputDirections.Add(Direction.DOWN);
            inputDirections.Add(Direction.RIGHT);
        }   
        if(left && !right && !up && down){
            pipeOrientation = PipeOrientation.DOWN_LEFT;
            inputDirections.Add(Direction.DOWN);
            inputDirections.Add(Direction.LEFT);
        }
        if(left && right && up && !down){
            pipeOrientation = PipeOrientation.HORIZONTAL_UP;
            inputDirections.Add(Direction.UP);
            inputDirections.Add(Direction.LEFT);
            inputDirections.Add(Direction.RIGHT);
        }   
        if(left && right && !up && down){
            pipeOrientation = PipeOrientation.HORIZONTAL_DOWN;
            inputDirections.Add(Direction.DOWN);
            inputDirections.Add(Direction.LEFT);
            inputDirections.Add(Direction.RIGHT);
        }   
        if(left && !right && up && down){
            pipeOrientation = PipeOrientation.VERTICAL_LEFT;
            inputDirections.Add(Direction.UP);
            inputDirections.Add(Direction.DOWN);
            inputDirections.Add(Direction.LEFT);
        }
        if(!left && right && up && down){
            pipeOrientation = PipeOrientation.VERTICAL_RIGHT;
            inputDirections.Add(Direction.UP);
            inputDirections.Add(Direction.DOWN);
            inputDirections.Add(Direction.RIGHT);
        }   
        building.spriteRenderer.sprite = pipeOrientationToSprite[(int)pipeOrientation];
    }
}
