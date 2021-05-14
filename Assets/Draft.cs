using System;
using System.Collections.Generic;
using UnityEngine;

public class Draft: MonoBehaviour
{

    public TowerSO[] pool;
    public List<TowerSO> current;
    private bool _selectionMade;
    private int _index;

    public event Action draftRefresh;
    public event Action<TowerSO> selectionChanged;
    
    public void OnClickableSelected(Clickable target)
    {
        if (!_selectionMade) return;

        var tile = target.GetComponent<Tile>();
        if (!tile) return;
        if (!tile.IsFree()) return;
        
        TowerSO towerSo = current[_index];
        tile.SpawnTower(towerSo);
        current.RemoveAt(_index);
        _selectionMade = false;
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
    }
}