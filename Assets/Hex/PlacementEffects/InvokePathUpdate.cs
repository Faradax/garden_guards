using System.Collections.Generic;

namespace Hex.PlacementEffects
{
public class InvokePathUpdate : TileBehaviour
{

    public GameEvent pathEditedEvent;
    
    public override void OnTilePlaced(List<Tile> neighbours)
    {
        pathEditedEvent.Invoke();
    }
    public override void OnTileRemoved()
    {
        pathEditedEvent.Invoke();
    }
}
}