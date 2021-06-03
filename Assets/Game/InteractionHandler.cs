using System;
using System.Threading.Tasks;
using Hex;
using UnityEngine;

namespace Game
{
public class InteractionHandler : MonoBehaviour
{

    private PlaceNewTileFlow placeNewTileFlow;
    private RemoveTileFlow removeTileFlow;

    private IInteraction _interaction;

    private Action _callback;

    public void OnEnable()
    {
        _interaction = null;
        placeNewTileFlow = GetComponent<PlaceNewTileFlow>();
        removeTileFlow = GetComponent<RemoveTileFlow>();
    }

    public void OnTileClicked(Clickable clickable)
    {
        if (_interaction == null) return;
        bool success = _interaction.OnTileClicked(clickable);
        if (success)
        {
            _callback.Invoke();
            _interaction = null;
        }
    }

    public void StartTilePlacement(TileSO tileSo, Action callback)
    {
        _callback = callback;
        _interaction = placeNewTileFlow;
        placeNewTileFlow.PlaceTile(tileSo);
    }

    public void StartTileRemoval(Action callback)
    {
        _callback = callback;
        _interaction = removeTileFlow;
    }
}
}