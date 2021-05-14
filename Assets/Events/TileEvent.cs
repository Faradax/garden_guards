using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Garden Guards/Tile Event", fileName = "")]
public class TileEvent : ScriptableObject
{
    public UnityEvent<Tile> @event;

    public void Invoke(Tile tile)
    {
        @event.Invoke(tile);
    }
}
