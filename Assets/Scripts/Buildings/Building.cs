using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingInfo buildingInfo;
    public SpriteRenderer spriteRenderer;
    public delegate void OnClick();
    public OnClick onClick;
    public bool isPipeConnectable = false;
    public delegate void Deposit(Item item);
    Deposit ReceiveADeposit_del;
    public List<ItemStack> buildingInvetory;
    public Dictionary<string, ItemStack> itemNameToBuildingInventorySlot;
    public MapTileGrid grid;
    public Direction inputTile;
    public Direction outputTile;
    
    void Start()
    {
        itemNameToBuildingInventorySlot = new Dictionary<string, ItemStack>();
        buildingInvetory = new List<ItemStack>();
        if(spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = buildingInfo.sprite;
        }
        if(ReceiveADeposit_del == null)
        {
            ReceiveADeposit_del = ReceiveADeposit;
        }
        if(onClick == null)
        {
            onClick = DefaultOnClick;
        }
    }

    void Update()
    {
        AttemptDeposit();
    }

    public void click()
    {
        if(onClick != null)
        {
            onClick();
        }
    }

    public void DefaultOnClick()
    {
        Debug.Log(buildingInvetory[0]);
    }
    // Just accepts the item and adds it to the inventory
    public void ReceiveADeposit(Item item)
    {
        AddItemToInventory(item);
    }

    public void AddItemToInventory(Item item)
    {
        if(itemNameToBuildingInventorySlot.ContainsKey(item.name)){
            itemNameToBuildingInventorySlot[item.name].amount += 1;
        }
        else{
            ItemStack newItemStack = new ItemStack(item, 1);
            itemNameToBuildingInventorySlot[item.name] = newItemStack;
            buildingInvetory.Add(newItemStack);
        }
    }
    // Default attempt deposit method just deposits first thing in inventory
    public void AttemptDeposit()
    {
        if(buildingInvetory.Count != 0){
            ItemStack toDepositFromStack = buildingInvetory[0];
            if(TryDepositOnNeighbor(toDepositFromStack.item)){
                if(toDepositFromStack.lowerAmount(1)){
                    buildingInvetory.Remove(toDepositFromStack);
                }
            }
        }
    }
    public bool CanAcceptDeposit(Direction itemSourceDirection)
    {
        return itemSourceDirection == inputTile;
    }
    // True on success, false on failure
    public bool TryDepositOnNeighbor(Item i)
    {
        // Get neighbor
        Vector2 gridPosition = grid.getTileCoord(new Vector2(this.transform.position.x, this.transform.position.y));
        Dictionary<Direction, MapTile> neighbors = grid.GetNeighboursOfTile(grid.GetTile(gridPosition));
        if(neighbors[outputTile] != null && neighbors[outputTile].buildingOnTile != null)
        {
            // found a neighbor! Let's try to deposit the item in them
            Building outputCandidate = neighbors[outputTile].buildingOnTile;
            if(outputCandidate.CanAcceptDeposit(Flip(outputTile))){
                // Looks like we are good to deposit
                outputCandidate.ReceiveADeposit_del(i);
                return true;
            }
            else{
                // Neighbor did not accept out deposit
                return false;
            }
        }
        // No neighbor building to deposit to
        else{
            return false;
        }
    }
    // Helper function to reverse directions... should be put in a util class
    public Direction Flip(Direction d){
        switch(d){
            case Direction.UP:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.UP;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
            default:
                return d;
        }
    }
}
