using System;
using System.Collections;
using Enemies;
using UnityEngine;
using UnityEngine.Events;

public class LevelFlow: MonoBehaviour
{
    public PathSpawner spawner;
    public int wavesCount = 3;
    public int currentWaveIndex = 0;
    public Counter enemyCounter;
    public Countdown preparationCountdown;

    public UnityEvent done; 
    private void Start()
    {
        StartCoroutine(FlowRoutine());
    }
    private IEnumerator FlowRoutine()
    {
        while (currentWaveIndex < wavesCount)
        {
            yield return RunWave();
            currentWaveIndex++;
        }
        done.Invoke();
    }
    
    private IEnumerator RunWave()
    {
        yield return DoPreparationCountdown();
        yield return LoopSpawn();
        yield return WaitForLastEnemyToDie();
    }
    private IEnumerator LoopSpawn()
    {

        return spawner.LoopSpawn();
    }

    private IEnumerator DoPreparationCountdown()
    {
        yield return preparationCountdown.Run();
    }
    private IEnumerator WaitForLastEnemyToDie()
    {
        yield return new WaitUntil(() => enemyCounter.Value == 0);
    }
}