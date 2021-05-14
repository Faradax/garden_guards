using System.Collections;
using PathCreation;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{

    public GameObject enemy;
    public PathCreator pathCreator;
    public float seconds = 3;
    private Coroutine _spawn;

    void OnEnable()
    {
        _spawn = StartCoroutine(LoopSpawn());
    }
    
    private IEnumerator LoopSpawn()
    {
        while (true)
        {
            GameObject enemyGameObject = Instantiate(enemy, transform.position, Quaternion.identity);
            var moveAlongPath = enemyGameObject.AddComponent<MoveAlongPath>();
            moveAlongPath.SetPath(pathCreator);
            yield return new WaitForSeconds(seconds);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_spawn);
    }
}
