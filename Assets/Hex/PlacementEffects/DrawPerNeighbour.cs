using System.Collections.Generic;
using UnityEngine;

public class DrawPerNeighbour : TileBehaviour
{
    public TileSO requiredNeighbour;

    public override void OnTilePlaced(List<Tile> neighbours)
    {
        neighbours.FindAll(tile => tile.tileSo == requiredNeighbour).ForEach(_ =>
            Draft.instance.DrawRandom(1));
    }
}