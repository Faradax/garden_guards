using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class Aura : MonoBehaviour
{
    public float radius = 2;
    private Tile _tile;

    private void OnEnable()
    {
        _tile = GetComponent<Tile>();
        _tile.tilePlaced.AddListener(UpdateBuffs);
        _tile.tileRemoved.AddListener(RemoveBuffs);
    }
    
    private void OnDisable()
    {
        _tile.tilePlaced.RemoveListener(UpdateBuffs);
        _tile.tileRemoved.RemoveListener(RemoveBuffs);
    }

    [UsedImplicitly]
    private void OnTilePlaced()
    {
        UpdateBuffs();
    }

    [UsedImplicitly]
    private void OnTileRemoved()
    {
        RemoveBuffs();
    }
    
    public void UpdateBuffs()
    {
        foreach (Buffable buffableNeighbour in GetBuffableNeighbours())
        {
            buffableNeighbour.ReceiveBuff(this, new Buff());
        }
    }

    private void RemoveBuffs()
    {
        foreach (Buffable buffableNeighbour in GetBuffableNeighbours())
        {
            buffableNeighbour.RemoveBuffs(this);
        }
    }

    public HashSet<Buffable> GetBuffableNeighbours()
    {
        Vector3 pos = _tile.transform.position;

        Collider[] result = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(pos, radius, result);
        HashSet<Buffable> neighbours = new();
        for (var i = 0; i < count; i++)
        {
            Collider collider = result[i];
            var neighbour = collider.GetComponent<Buffable>();
            if (neighbour && neighbour != _tile)
            {
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }
}
