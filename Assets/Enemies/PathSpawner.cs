using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;
using UnityEngine.Events;

public class PathSpawner : MonoBehaviour
{

    public GameObject enemy;
    public PathCreator pathCreator;
    public AllTiles allTiles;
    public float seconds = 2;
    public float amount = 10;
    public UnityEvent done;
    
    void OnEnable()
    {
        OnPathsEdited();
    }
    
    public IEnumerator LoopSpawn()
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

    public void OnPathsEdited()
    {
        List<PathCandidate> pathCandidates = allTiles.FindPaths();
        pathCandidates.Sort((c1, c2) => c1.Length.CompareTo(c2.Length));
        DetermineIrreplaceable(pathCandidates);
        Vector3[] points = pathCandidates.First().path.Select(tile => tile.transform.position).ToArray();
        var pathCreatorBezierPath = new BezierPath(points, false, PathSpace.xz);
        pathCreatorBezierPath.ControlPointMode = BezierPath.ControlMode.Automatic;
        pathCreator.bezierPath = pathCreatorBezierPath;
    }
    private static void DetermineIrreplaceable(List<PathCandidate> pathCandidates)
    {

        foreach (Tile tile in pathCandidates.SelectMany(candidate => candidate.path))
        {
            tile.isIrreplaceable = pathCandidates.All(candidate => candidate.path.Contains(tile));
        }
    }
}
