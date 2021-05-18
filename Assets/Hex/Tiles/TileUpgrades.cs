using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Tile Upgrades", fileName = "TileUpgrades")]
public class TileUpgrades : ScriptableObject
{
    public List<Upgrade> upgrades; 
    
    [Serializable]
    public struct Upgrade
    {
        public TowerSO via;
        public TowerSO to;
    }
}
