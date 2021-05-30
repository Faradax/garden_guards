using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeCondition : MonoBehaviour
{
    public TileSO upgradeTo;
    public List<TileSO> requiredTilesAdjacent;
    
    [SerializeField]
    [HideInInspector]
    private bool isFulfilled;
    
    [SerializeField]
    [HideInInspector]
    private List<Tile> upgradeNeighbours;

    public bool IsFulfilled()
    {
        return isFulfilled;
    }
    public void EvaluateUpgradeCondition(List<Tile> neighbours)
    {
        List<TileSO> remainingRequiredNeighbours = new List<TileSO>(requiredTilesAdjacent);
        List<Tile> remainingAvailableNeighbours = neighbours.Where(it => it != null).ToList();
        
        upgradeNeighbours.Clear();
        foreach (Tile availableNeighbour in remainingAvailableNeighbours)
        {
            if (remainingRequiredNeighbours.Remove(availableNeighbour.tileSo))
            {
                upgradeNeighbours.Add(availableNeighbour);
            }
        }
        isFulfilled = remainingRequiredNeighbours.Count == 0;
    }
    public void DoUpgrade()
    {
        if (!isFulfilled)
        {
            throw new Exception("DoUpgrade called on not-possible upgrade - Checks missing somewhere?");
        }
        foreach (Tile upgradeNeighbour in new List<Tile>(upgradeNeighbours))
        {
            HexMap.instance.RemoveTile(upgradeNeighbour);
        }
        HexMap.instance.ReplaceTile(GetComponent<Tile>(), upgradeTo);
    }
}