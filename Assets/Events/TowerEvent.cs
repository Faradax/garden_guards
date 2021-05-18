using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Garden Guards/Tower Event", fileName = "")]
public class TowerEvent : ScriptableObject
{
    public UnityEvent<TileSO> @event;

    public void Invoke(TileSO tile)
    {
        @event.Invoke(tile);
    }
}
