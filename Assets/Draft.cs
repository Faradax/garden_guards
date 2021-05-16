using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class Draft: MonoBehaviour
{

    public List<TowerSO> pool;
    public List<TowerSO> current;
    private bool _selectionMade;
    private int _index;

    public event Action draftRefresh;
    public UnityEvent<TowerSO> selectionChanged;
    public UnityEvent<TowerSO> towerPlaced;

    private readonly Random _random = new();

    public void OnWaveStart()
    {
        selectionChanged.Invoke(null);
        _selectionMade = false;
        current.Clear();
        for (var i = 0; i < 7; i++)
        {
            current.Add(RandomFromPool());
        }
        draftRefresh?.Invoke();
    }
    private TowerSO RandomFromPool()
    {
        int max = pool.Count;
        int randomIndex = _random.Next(max);
        return pool[randomIndex];
    }

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