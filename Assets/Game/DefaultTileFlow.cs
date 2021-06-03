using Hex;
using UnityEngine;

namespace Game
{
public class DefaultTileFlow: MonoBehaviour, IInteraction
{

    public bool OnTileClicked(Clickable clickable)
    {
        var tile = clickable.GetComponent<Tile>();
        if (tile == null) return false;
        
        tile.OnClick();
        return true;
    }
}
}