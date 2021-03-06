using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Garden Guards/Game Event", fileName = "")]
public class GameEvent : ScriptableObject
{
    public UnityEvent Event;

    public void Invoke()
    {
        Event.Invoke();
    }
}
