using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionModuleFactory : MonoBehaviour
{
    public static ConstructionModule DepositorCM;
    public ConstructionModule DepositorCMInput;
    public static Building PipeCMPrefab;
    public static MapTileGrid grid;

    public void Start()
    {
        DepositorCM = DepositorCMInput;
    }
    // Does what it says. Called by shop
    public static void InstantiateConstructionModule(string buildingName){
         ConstructionModule cm;
        switch(buildingName)
        {
            case "Depositor":
                cm = Instantiate(
                        DepositorCM,
                        new Vector3(0.0f, 0.0f, -2.0f),
                        Quaternion.identity);
                cm.grid = grid;
                break;
            case "Pipe":
                Instantiate(
                        PipeCMPrefab,
                        new Vector3(0.0f, 0.0f, -2.0f),
                        Quaternion.identity);
                break;
            default:
                Debug.LogError("Invalid buildingName Selection: " + buildingName);
                break;
        }
    }
}
