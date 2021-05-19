using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Tower")]
public class TileSO : ScriptableObject
{

    public bool isLandscape;
    
    public GameObject asset;
    public TileUpgrades upgrades;
    public bool canUpgradeTo(TileSO tileSo)
    {
        // TODO: Probably return upgrade later
        if (tileSo.isLandscape) return true;
        if (!upgrades) return false;
        return upgrades.upgrades.Any(it => it.via == tileSo);
    }
    public TileSO GetUpgradeFor(TileSO tileSo)
    {
        if (!upgrades) return null;
        TileUpgrades.Upgrade foundUpgrade = upgrades.upgrades.Where(it => it.via == tileSo).FirstOrDefault();
        TileSO upgradeTo = foundUpgrade.to;
        if (!upgradeTo) return tileSo;
        return upgradeTo;
    }
}
