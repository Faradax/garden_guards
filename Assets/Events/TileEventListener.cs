using UnityEngine;
using UnityEngine.Events;

public class TileEventListener : MonoBehaviour
{
    
    public TileEvent tileEvent;

    public UnityEvent<Tile> action;
    
    void Start()
    {
        tileEvent.@event.AddListener(action.Invoke);
    }
}
