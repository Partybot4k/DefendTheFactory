using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItem : MonoBehaviour
{
    public float speed;
    public Vector3 directionVector;
    public Item item;
    public SpriteRenderer spriteRenderer;
    public Direction direction; // just for debugging rn
    void Start()
    {
        spriteRenderer.sprite = item.icon;
    }

    void Update()
    {
    }
    public void setDirection(Direction dir){
        switch(dir){
            case Direction.UP:
                directionVector = Vector3.up;
                break;
            case Direction.DOWN:
                directionVector = Vector3.down;
                break;
            case Direction.LEFT:
                directionVector = Vector3.left;
                break;
            case Direction.RIGHT:
                directionVector = Vector3.right;
                break;
        }
        direction = dir;
    }
}
