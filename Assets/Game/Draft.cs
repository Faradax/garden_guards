using System;
using System.Collections.Generic;
using Game;
using Hex;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class Draft: MonoBehaviour
{

    [Serializable]
    public class ShopItem
    {
        public ShopItem(TileSO tileSo)
        {
            TileSO = tileSo;
        }
        public TileSO TileSO { get; }
    }
    
    public List<TileSO> pool;
    [HideInInspector]
    public List<ShopItem> current;
    [HideInInspector]
    public bool selectionMade;

    public event Action draftRefresh;
    public UnityEvent<TileSO> selectionChanged;

    private readonly Random _random = new();
    public int draw = 7;

    public InteractionHandler interactionHandler;
    private int _index;

    public static Draft instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnWaveStart()
    {
        selectionChanged.Invoke(null);
        selectionMade = false;
        current.Clear();
        DrawRandom(draw);
    }
    public void DrawRandom(int amount)
    {

        for (var i = 0; i < amount; i++)
        {
            current.Add(new ShopItem(RandomFromPool()));
        }
        draftRefresh?.Invoke();
    }
    
    private TileSO RandomFromPool()
    {
        int max = pool.Count;
        int randomIndex = _random.Next(max);
        return pool[randomIndex];
    }

    public IEnumerable<ShopItem> GetItems()
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
        selectionMade = true;
        TileSO selectedTileSO = current[index].TileSO;
        selectionChanged.Invoke(selectedTileSO);
        if (selectedTileSO.name == "VoidTile")
        {
            interactionHandler.StartTileRemoval(OnTilePlaced);
        }
        else
        {
            interactionHandler.StartTilePlacement(selectedTileSO, OnTilePlaced);
        }
    }

    public void OnTilePlaced()
    {
        current.RemoveAt(_index);
        _index = -1;
        selectionMade = false;
        draftRefresh?.Invoke();
        selectionChanged.Invoke(null);
    }

    public void Remove(ShopItem item)
    {
        current.Remove(item);
        draftRefresh?.Invoke();
    }
    
    private void ClearSelection()
    {

        _index = -1;
        selectionMade = false;
        selectionChanged.Invoke(null);
        interactionHandler.Abort();
    }
}