using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingInfo buildingInfo;
    public SpriteRenderer spriteRenderer;
    public delegate void OnClick();
    public OnClick onClick;
    
    void Start()
    {
        spriteRenderer.sprite = buildingInfo.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click()
    {
        if(onClick != null)
        {
            onClick();
        }
    }
}
