using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public PathCreator pathCreator;
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
            var moveAlongPath = enemyGameObject.GetComponent<MoveAlongPath>();
            moveAlongPath.SetPath(pathCreator);
            yield return new WaitForSeconds(3);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_spawn);
    }
}
