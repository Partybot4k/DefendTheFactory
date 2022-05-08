using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingInfo buildingInfo;
    public SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer.sprite = buildingInfo.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
