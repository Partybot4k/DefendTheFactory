using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItem : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Item item;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer.sprite = item.icon;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public void setDirection(Direction dir){
        switch(dir){
            case Direction.UP:
                direction = Vector3.up;
                break;
            case Direction.DOWN:
                direction = Vector3.down;
                break;
            case Direction.LEFT:
                direction = Vector3.left;
                break;
            case Direction.RIGHT:
                direction = Vector3.right;
                break;
        }
    }
}
