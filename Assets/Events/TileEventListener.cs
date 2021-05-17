using System;
using UnityEngine;
using UnityEngine.Events;

public class TileEventListener : MonoBehaviour
{
    
    public TileEvent tileEvent;

    public UnityEvent<Tile> action;
    
    void OnEnable()
    {
        tileEvent.@event.AddListener(action.Invoke);
    }

    private void OnDisable()
    {
        tileEvent.@event.RemoveListener(action.Invoke);
    }
}
