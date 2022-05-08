using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuildingInfo", menuName = "BuildingInfo")]
public class BuildingInfo : ScriptableObject
{
    public string buildingName;
    public int width;
    public int height;
    public Sprite sprite;
}
