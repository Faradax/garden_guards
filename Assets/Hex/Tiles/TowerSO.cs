using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Tower")]
public class TowerSO : ScriptableObject
{

    public bool isLandscape;
    
    public GameObject asset;
    public TileUpgrades upgrades;
    public bool canUpgradeTo(TowerSO towerSo)
    {
        // TODO: Probably return upgrade later
        if (towerSo.isLandscape) return true;
        if (!upgrades) return false;
        return upgrades.upgrades.Any(it => it.via == towerSo);
    }
}
