using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
[CreateAssetMenu(fileName = "WaveData", menuName = "Garden Guards/WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public LaneSO[] lanes;

    public List<Spawn> spawns = new List<Spawn>
    {
        new Spawn()
    };
}

[Serializable]
public class Spawn
{
    public LaneSO lane;
    public float StartTime { get; } = 15;
    public float EndTime { get; } = 65;
    public int Amount { get; } = 9;
}
}