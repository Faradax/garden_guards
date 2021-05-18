using Enemies;
using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour
{

    public Counter enemyCounter;
    public UnityEvent winConditionFulfilled;

    private bool _spawningDone;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        enemyCounter.valueZero.AddListener(OnNoEnemiesOnMap);
    }

    private void OnDisable()
    {
        enemyCounter.valueZero.RemoveListener(OnNoEnemiesOnMap);
    }

    public void OnSpawningDone()
    {
        _spawningDone = true;
    }
    
    private void OnNoEnemiesOnMap()
    {
        if (_spawningDone)
        {
            winConditionFulfilled.Invoke();
        }
    }
    
}
