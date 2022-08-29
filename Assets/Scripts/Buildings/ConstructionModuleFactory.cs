using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionModuleFactory : MonoBehaviour
{
    public static ConstructionModule DepositorCM;
    public ConstructionModule DepositorCMInput;
    public MapTileGrid gridInput;
    public static ConstructionModule PipeCMPrefab;
    public ConstructionModule PipeCMPrefabInput;
    public static ConstructionModule AmmoFactoryCMPrefab;
    public ConstructionModule AmmoFactoryCMPrefabInput;
    public static ConstructionModule TurretCMPrefab;
    public ConstructionModule TurretCMPrefabInput;
    public static MapTileGrid grid;

    public void Start()
    {
        DepositorCM = DepositorCMInput;
        PipeCMPrefab = PipeCMPrefabInput;
        AmmoFactoryCMPrefab = AmmoFactoryCMPrefabInput;
        TurretCMPrefab = TurretCMPrefabInput;
        grid = gridInput;
    }
    // Does what it says. Called by shop
    public static void InstantiateConstructionModule(string buildingName){
        ConstructionModule cm = null;
        switch(buildingName)
        {
            case "Depositor":
                cm = Instantiate(
                        DepositorCM,
                        new Vector3(0.0f, 0.0f, -2.0f),
                        Quaternion.identity);
                break;
            case "Pipe":
                cm = Instantiate(
                        PipeCMPrefab,
                        new Vector3(0.0f, 0.0f, -2.0f),
                        Quaternion.identity);
                break;
            case "AmmoFactory":
                cm = Instantiate(
                        AmmoFactoryCMPrefab,
                        new Vector3(0.0f, 0.0f, -2.0f),
                        Quaternion.identity);
                break;
            case "Turret":
                cm = Instantiate(
                        TurretCMPrefab,
                        new Vector3(0.0f, 0.0f, -2.0f),
                        Quaternion.identity);
                break;
            default:
                Debug.LogError("Invalid buildingName Selection: " + buildingName);
                return;
        }
        cm.grid = grid;
    }
}
