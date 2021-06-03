using System.Collections.Generic;
using UnityEngine;

public abstract class TileBehaviour : MonoBehaviour
{
    public virtual void OnTilePlaced(List<Tile> neighbours) {}
    public virtual void OnTileRemoved() {}
}