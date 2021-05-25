using System;
using System.Collections.Generic;
using Hex;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class Draft: MonoBehaviour
{

    public List<TileSO> pool;
    public List<TileSO> current;
    private bool _selectionMade;
    private int _index;

    public event Action draftRefresh;
    public UnityEvent<TileSO> selectionChanged;
    public UnityEvent<TileSO> towerPlaced;

    private readonly Random _random = new();
    public int draw = 7;

    // TODO: DEFINITELY get this out of here
    public HexMap hexMap;
    
    public void OnWaveStart()
    {
        selectionChanged.Invoke(null);
        _selectionMade = false;
        current.Clear();
        for (var i = 0; i < draw; i++)
        {
            current.Add(RandomFromPool());
        }
        draftRefresh?.Invoke();
    }
    private TileSO RandomFromPool()
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

        TileSO tileSo = current[_index];
        if (!tile.IsEligible(tileSo)) return;
        
        //tile.SpawnTower(tileSo);
        Vector3 transformPosition = target.transform.position;
        hexMap.SetHexTile(AxialHexCoords.FromXZ(transformPosition.x, transformPosition.z), tileSo);
        current.RemoveAt(_index);
        _selectionMade = false;
        towerPlaced.Invoke(tileSo);
        draftRefresh?.Invoke();
    }

    public IEnumerable<TileSO> GetItems()
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