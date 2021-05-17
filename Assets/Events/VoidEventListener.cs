using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
    
    public GameEvent gameEvent;

    public UnityEvent action;
    
    void OnEnable()
    {
        gameEvent.Event.AddListener(action.Invoke);
    }
    
    void OnDisable()
    {
        gameEvent.Event.RemoveListener(action.Invoke);
    }
}
