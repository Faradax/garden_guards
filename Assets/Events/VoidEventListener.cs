using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
    
    public GameEvent gameEvent;

    public UnityEvent action;
    
    void Start()
    {
        gameEvent.Event.AddListener(action.Invoke);
    }
}
