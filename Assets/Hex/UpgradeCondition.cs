using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    private bool _isUprading;

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
        if (_isUprading) return;
        if (!isFulfilled)
        {
            throw new Exception("DoUpgrade called on not-possible upgrade - Checks missing somewhere?");
        }
        _isUprading = true;
        StartCoroutine(UpgradeRoutine());
    }
    private IEnumerator UpgradeRoutine()
    {
        foreach (Tile upgradeNeighbour in new List<Tile>(upgradeNeighbours))
        {
            HexMap.instance.ForceRemoveTile(upgradeNeighbour);
            yield return new WaitForSeconds(0.2f);
        }
        HexMap.instance.ReplaceTile(GetComponent<Tile>(), upgradeTo);
    }
}