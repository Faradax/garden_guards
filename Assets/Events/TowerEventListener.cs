using UnityEngine;
using UnityEngine.Events;

public class TowerEventListener : MonoBehaviour
{
    
    public TowerEvent towerEvent;

    public UnityEvent<TowerSO> action;
    
    void Start()
    {
        towerEvent.@event.AddListener(action.Invoke);
    }
}