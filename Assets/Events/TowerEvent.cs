using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Garden Guards/Tower Event", fileName = "")]
public class TowerEvent : ScriptableObject
{
    public UnityEvent<TowerSO> @event;

    public void Invoke(TowerSO tower)
    {
        @event.Invoke(tower);
    }
}
