using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Enemy")]
public class EnemySO : ScriptableObject
{
    public GameObject asset;
    public int maxHealth;

    public Counter counter;

    public int lootAmount;
    public GameObject lootAsset;
}
