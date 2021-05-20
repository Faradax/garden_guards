using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Tower")]
public class TileSO : ScriptableObject
{

    public bool placeAnywhere;
    
    public GameObject asset;
    public TileUpgrades upgrades;
    public bool canUpgradeTo(TileSO tileSo)
    {
        // TODO: Probably return upgrade later
        if (tileSo.placeAnywhere) return true;
        if (!upgrades) return false;
        return upgrades.upgrades.Any(it => it.via == tileSo);
    }
    public TileSO GetUpgradeFor(TileSO tileSo)
    {
        if (!upgrades) return tileSo;
        TileUpgrades.Upgrade foundUpgrade = upgrades.upgrades.FirstOrDefault(it => it.via == tileSo);
        TileSO upgradeTo = foundUpgrade.to;
        if (!upgradeTo) return tileSo;
        return upgradeTo;
    }
}
