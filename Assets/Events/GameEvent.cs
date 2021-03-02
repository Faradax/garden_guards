using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Game Event", fileName = "")]
public class GameEvent : ScriptableObject
{
    public event Action Event;

    public void Invoke()
    {
        Event.Invoke();
    }
}
