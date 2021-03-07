using UnityEngine;

public class EventTester : MonoBehaviour
{
    public GameEvent gameOverEvent;
    
    void Start()
    {
        gameOverEvent.Event.AddListener(() => Debug.Log("Game is over now :("));
    }
}
