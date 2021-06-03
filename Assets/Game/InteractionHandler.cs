using Hex;
using UnityEngine;

namespace Game
{
public class InteractionHandler: MonoBehaviour
{

    private PlaceNewTileFlow placeNewTileFlow;
    private RemoveTileFlow removeTileFlow;
    
    private IInteraction _interaction;
    
    public void OnEnable()
    {
        _interaction = null;
        placeNewTileFlow = GetComponent<PlaceNewTileFlow>();
        removeTileFlow = GetComponent<RemoveTileFlow>();
    }

    public void OnTileClicked(Clickable clickable)
    {
        _interaction?.OnTileClicked(clickable);
    }

    public void StartTilePlacement(TileSO tileSo)
    {
        _interaction = placeNewTileFlow;
        placeNewTileFlow.PlaceTile(tileSo);
    }
    
    public void StartTileRemoval()
    {
        _interaction = removeTileFlow;
    }
}
}