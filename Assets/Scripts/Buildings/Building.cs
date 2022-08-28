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
    public delegate void Deposit(Direction itemSourceDirection, Item item);
    public Deposit ReceiveADeposit_del;
    public delegate bool CanAcceptDeposit(Direction itemSourceDirection, Item item);
    public CanAcceptDeposit CanAcceptDeposit_del;
    public Dictionary<string, ItemStack> itemNameToBuildingInventorySlot;
    public MapTileGrid grid;
    public Direction inputTile = Direction.RIGHT;
    public Direction outputTile = Direction.LEFT;
    public List<string> inputWhiteList = new List<string>(); // If this list is not empty, building will only accept these items. It uses names
    public List<string> outputWhiteList = new List<string>();
    void Start()
    {
        itemNameToBuildingInventorySlot = new Dictionary<string, ItemStack>();
        if(spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = buildingInfo.sprite;
        }
        if(ReceiveADeposit_del == null)
        {
            ReceiveADeposit_del = ReceiveADeposit;
        }
        if(CanAcceptDeposit_del == null)
        {
            CanAcceptDeposit_del = CanAcceptDepositDefault;
        }
        if(onClick == null)
        {
            onClick = DefaultOnClick;
        }
        inputTile = Direction.RIGHT;
        outputTile = Direction.LEFT;
    }

    void Update()
    {
        AttemptDepositOnOtherBuilding();
    }

    public void click()
    {
        if(onClick != null)
        {
            onClick();
        }
    }
    // just logging
    public void DefaultOnClick()
    {
        foreach(string key in itemNameToBuildingInventorySlot.Keys){
            Debug.Log(itemNameToBuildingInventorySlot[key].ToString());
        }
    }
        /**
    ===============inventory methods=============
    These are the methods for interfacing with building inventory
    */
    // Just adds item to inventory
    public void AddItemToInventory(Item item, int count)
    {
        if(itemNameToBuildingInventorySlot.ContainsKey(item.name)){
            itemNameToBuildingInventorySlot[item.name].amount += count;
        }
        else{
            ItemStack newItemStack = new ItemStack(item, count);
            itemNameToBuildingInventorySlot[item.name] = newItemStack;
        }
    }
    // just remove the item
    public void removeItemFromInventory(Item item, int count){
        if(itemNameToBuildingInventorySlot.ContainsKey(item.name) && itemNameToBuildingInventorySlot[item.name].amount > 0){
            itemNameToBuildingInventorySlot[item.name].amount -= count;
            // no negatives
            if (itemNameToBuildingInventorySlot[item.name].amount < 0){
                itemNameToBuildingInventorySlot[item.name].amount = 0;
            }
        }
    }

    public void AddItemStackToInventory(ItemStack itemStack)
    {
        AddItemToInventory(itemStack.item, itemStack.amount);
    }

    public void RemoveItemStackToInventory(ItemStack itemStack)
    {
        removeItemFromInventory(itemStack.item, itemStack.amount);
    }
    /**
    ===============deposit methods=============
    These govern default behavior for an item being deposted in the building from another building
    */
    // Just accepts the item and adds it to the inventory
    public void ReceiveADeposit(Direction itemSourceDirection, Item item)
    {
        AddItemToInventory(item, 1);
    }
    // Default attempt deposit method just deposits first thing in inventory, respecting output whitelist
    //This is so fucking ugly lol
    public void AttemptDepositOnOtherBuilding()
    {
        if(itemNameToBuildingInventorySlot.Keys.Count != 0){
            foreach(string key in itemNameToBuildingInventorySlot.Keys){
                if(itemNameToBuildingInventorySlot[key].amount > 0 && (outputWhiteList.Count == 0 || outputWhiteList.Contains(itemNameToBuildingInventorySlot[key].item.name))){
                    if(TryDepositOnNeighbor(itemNameToBuildingInventorySlot[key].item, outputTile)){
                        removeItemFromInventory(itemNameToBuildingInventorySlot[key].item, 1);
                    }               
                }
            }
        }
    }
    // Is it whitelisted? Are we receiving from the right direction?
    public bool CanAcceptDepositDefault(Direction itemSourceDirection, Item item)
    {
        return itemSourceDirection == inputTile && (inputWhiteList.Count == 0 || inputWhiteList.Contains(item.name));
    }
    // True on success, false on failure
    // Uses CanAcceptDeposit_del and ReceiveADeposit_del
    public bool TryDepositOnNeighbor(Item i, Direction outputDirection)
    {
        // Get neighbor
        Vector2 gridPosition = grid.getTileCoord(new Vector2(this.transform.position.x, this.transform.position.y));
        Dictionary<Direction, MapTile> neighbors = grid.GetNeighboursOfTile(grid.GetTile(gridPosition));
        if(neighbors[outputDirection] != null && neighbors[outputDirection].buildingOnTile != null)
        {
            // found a neighbor! Let's try to deposit the item in them
            Building outputCandidate = neighbors[outputDirection].buildingOnTile;
            if(outputCandidate.CanAcceptDeposit_del(Flip(outputDirection), i)){
                // Looks like we are good to deposit
                outputCandidate.ReceiveADeposit_del(Flip(outputDirection), i);
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
    public static Direction Flip(Direction d){
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
