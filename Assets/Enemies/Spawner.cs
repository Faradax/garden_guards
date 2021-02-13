using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public VectorMapper vectorMapper;
    public float seconds = 3;
    private Coroutine _spawn;

    void Start()
    {
        _spawn = StartCoroutine(LoopSpawn());
    }
    
    private IEnumerator LoopSpawn()
    {
        while (true)
        {
            GameObject enemyGameObject = Instantiate(enemy, transform.position, Quaternion.identity);
            var moveAlongPath = enemyGameObject.GetComponent<MoveInVectorField>();
            moveAlongPath.SetVectorMapper(vectorMapper);
            yield return new WaitForSeconds(seconds);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_spawn);
    }
}
