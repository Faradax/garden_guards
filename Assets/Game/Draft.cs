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

    public event Action draftRefresh;
    public UnityEvent<TileSO> selectionChanged;

    private readonly Random _random = new();
    public int draw = 7;

    public PlaceNewTileFlow placeNewTileFlow;
    private int _index;

    public static Draft instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnWaveStart()
    {
        selectionChanged.Invoke(null);
        _selectionMade = false;
        current.Clear();
        DrawRandom(draw);
    }
    public void DrawRandom(int amount)
    {

        for (var i = 0; i < amount; i++)
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

    public IEnumerable<TileSO> GetItems()
    {
        return current;
    }

    public void ChangeSelection(int index)
    {
        if (index == _index)
        {
            ClearSelection();
        }
        else
        {
            UpdateSelection(index);
        }
    }
    private void UpdateSelection(int index)
    {

        _index = index;
        _selectionMade = true;
        selectionChanged.Invoke(current[index]);
        placeNewTileFlow.PlaceTile(current[index]);
    }

    public void OnTowerPlaced()
    {
        current.RemoveAt(_index);
        _index = -1;
        _selectionMade = false;
        draftRefresh?.Invoke();
        selectionChanged.Invoke(null);
    }
    
    private void ClearSelection()
    {

        _index = -1;
        _selectionMade = false;
        selectionChanged.Invoke(null);
        placeNewTileFlow.Abort();
    }
}