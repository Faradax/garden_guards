using System;
using Hex;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
public class InteractionHandler : MonoBehaviour
{

    private PlaceNewTileFlow placeNewTileFlow;
    private RemoveTileFlow removeTileFlow;
    private DefaultTileFlow defaultTileFlow;

    private IInteraction _interaction;

    private Action _callback;

    public void OnEnable()
    {
        placeNewTileFlow = GetComponent<PlaceNewTileFlow>();
        removeTileFlow = GetComponent<RemoveTileFlow>();
        defaultTileFlow = GetComponent<DefaultTileFlow>();
        _interaction = defaultTileFlow;
    }

    public void OnTileClicked(Clickable clickable)
    {
        bool success = _interaction.OnTileClicked(clickable);
        if (success)
        {
            _callback?.Invoke();
            Reset();
        }
    }

    public void OnTileHovered(Tile tile)
    {
        _interaction.OnTileHovered(tile);
    }
    
    [UsedImplicitly]
    // Event Target for PlayerInput.
    public void OnMouseWheel(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        int diff = (int) ctx.ReadValue<Vector2>().y;
        
        // No idea why there are diffs of value 0 in phase "performed"
        if (diff == 0) return;
        
        _interaction.OnMouseWheel(diff);
    }
    
    private void Reset()
    {

        _callback = null;
        _interaction = defaultTileFlow;
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
    public void Abort()
    {
        Reset();
    }
}
}