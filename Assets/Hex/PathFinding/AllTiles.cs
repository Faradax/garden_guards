using System.Collections.Generic;
using UnityEngine;

public class AllTiles : MonoBehaviour
{
    public HexMap hexMap;
    public Tile start;
    public Tile end;

    public List<PathCandidate> FindPaths()
    {
        Tile from = start, to = end;
        List<PathCandidate> candidates = new();
        List<PathCandidate> winners = new();
        // Initialize candidate: Start Tile
        candidates.Add(new PathCandidate(from));

        List<PathCandidate> nextGeneration = new();
        // As long as goal is not reached
        while (candidates.Count != 0)
        {
            foreach (PathCandidate pathCandidate in candidates)
            {
                if (pathCandidate.Tip == to)
                {
                    winners.Add(pathCandidate);
                    continue;
                }

                var tileNeighbours = new HashSet<Tile>(hexMap.TileNeighbours(pathCandidate.Tip));
                foreach (Tile neighbour in tileNeighbours)
                {
                    if (!neighbour.isPath) continue;

                    PathCandidate extendedCandidate = pathCandidate.ExtendedBy(neighbour);
                    if (extendedCandidate.Length <= pathCandidate.Length) continue;

                    nextGeneration.Add(extendedCandidate);
                }
            }
            candidates = nextGeneration;
            nextGeneration = new List<PathCandidate>();
        }
        return winners;
    }

    public HashSet<Tile> GetTileNeighbours(Tile tile)
    {
        Vector3 pos = tile.transform.position;

        Collider[] result = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(pos, 1f, result);
        HashSet<Tile> neighbours = new();
        for (var i = 0; i < count; i++)
        {
            Collider collider = result[i];
            var neighbour = collider.GetComponent<Tile>();
            if (neighbour && neighbour != tile)
            {
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }
}