using JetBrains.Annotations;
using UnityEngine;

public class PlacementEffect : MonoBehaviour, ITileLifecycleAware
{
    public void OnTilePlaced()
    {
        Debug.Log("Hell yeah");
    }
    public void OnTileRemoved()
    {
    }
}