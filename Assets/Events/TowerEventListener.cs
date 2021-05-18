using UnityEngine;
using UnityEngine.Events;

public class TowerEventListener : MonoBehaviour
{
    
    public TowerEvent towerEvent;

    public UnityEvent<TileSO> action;
    
    void OnEnable()
    {
        towerEvent.@event.AddListener(action.Invoke);
    }

    private void OnDisable()
    {
        towerEvent.@event.RemoveListener(action.Invoke);
    }
}