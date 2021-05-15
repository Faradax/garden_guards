using System.Collections;
using PathCreation;
using UnityEngine;
using UnityEngine.Events;

public class PathSpawner : MonoBehaviour
{

    public GameObject enemy;
    public PathCreator pathCreator;
    public float seconds = 2;
    public float amount = 10;
    public UnityEvent done;
    
    private Coroutine _spawn;

    void OnEnable()
    {
        _spawn = StartCoroutine(LoopSpawn());
    }
    
    private IEnumerator LoopSpawn()
    {
        var spawned = 0;
        while (spawned < amount)
        {
            GameObject enemyGameObject = Instantiate(enemy, transform.position, Quaternion.identity);
            var moveAlongPath = enemyGameObject.AddComponent<MoveAlongPath>();
            moveAlongPath.SetPath(pathCreator);
            spawned++;
            yield return new WaitForSeconds(seconds);
        }
        done.Invoke();
    }

    private void OnDisable()
    {
        StopCoroutine(_spawn);
    }
}
