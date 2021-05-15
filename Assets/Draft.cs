using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Draft: MonoBehaviour
{

    public TowerSO[] pool;
    public List<Shopable> pool2;
    public List<TowerSO> current;
    private bool _selectionMade;
    private int _index;

    public event Action draftRefresh;
    public UnityEvent<TowerSO> selectionChanged;
    public UnityEvent<TowerSO> towerPlaced;
    
    public void OnClickableSelected(Clickable target)
    {
        if (!_selectionMade) return;

        var tile = target.GetComponent<Tile>();
        if (!tile) return;

        TowerSO towerSo = current[_index];
        if (!tile.IsEligible(towerSo)) return;
        
        tile.SpawnTower(towerSo);
        current.RemoveAt(_index);
        _selectionMade = false;
        towerPlaced.Invoke(towerSo);
        draftRefresh?.Invoke();
    }

    public IEnumerable<TowerSO> GetItems()
    {
        return current;
    }

    public void ChangeSelection(int index)
    {
        _selectionMade = true;
        _index = index;
        selectionChanged.Invoke(current[index]);
    }
}