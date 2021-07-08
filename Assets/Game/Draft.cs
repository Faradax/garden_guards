using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class Draft : MonoBehaviour
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
    public List<ShopItem> deck;

    [HideInInspector]
    public List<ShopItem> current;

    [HideInInspector]
    public ShopItem selected;

    public event Action draftRefresh;
    public UnityEvent<TileSO> selectionChanged;

    private readonly Random _random = new();
    public int draw = 7;

    public InteractionHandler interactionHandler;

    public static Draft instance;

    private void Awake()
    {
        instance = this;
        var decksize = 64;
        for (var i = 0; i < decksize; i++)
        {
            deck.Add(new ShopItem(pool[i%pool.Count]));
        }
    }

    public void OnWaveStart()
    {
        selectionChanged.Invoke(null);
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

    private ShopItem RandomFromPool()
    {
        int max = deck.Count;
        int randomIndex = _random.Next(max);
        var tile = deck[randomIndex];
        deck.RemoveAt(randomIndex);
        return tile;
    }

    public IEnumerable<ShopItem> GetItems()
    {
        return current;
    }

    public void ChangeSelection(ShopItem item)
    {
        if (selected == item)
        {
            ClearSelection();
        }
        else
        {
            UpdateSelection(item);
        }
    }
    private void UpdateSelection(ShopItem item)
    {

        selected = item;
        TileSO selectedTileSO = item.TileSO;
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
        current.Remove(selected);
        selected = null;
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

        selected = null;
        selectionChanged.Invoke(null);
        interactionHandler.Abort();
    }
}