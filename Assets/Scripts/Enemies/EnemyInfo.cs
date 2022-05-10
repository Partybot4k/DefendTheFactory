using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Type")]
public class EnemyInfo : ScriptableObject
{
    public float baseHealth;
    public float speed;
    public int atWallTimer;
    public int wallDamage;
}
