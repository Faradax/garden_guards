using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Tower")]
public class TowerSO : ScriptableObject
{
    public GameObject asset;

    public List<TileSO> compatibleTiles; 
    public bool IsCompatible(Tile tile)
    {
        return compatibleTiles.Contains(tile.tileSo);
    }
}
