using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public Building b;
    public Item ammo;
    void Start()
    {
        b.inputWhiteList.Add(ammo.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
