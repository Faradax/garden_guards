using UnityEngine;

namespace Hex
{
public class RemoveTileFlow : MonoBehaviour, IInteraction
{
    public HexMap hexMap;

    public bool OnTileClicked(Clickable target)
    {
        return RemoveTile(target);
    }
    private bool RemoveTile(Clickable target)
    {
        Vector3 transformPosition = target.transform.position;
        AxialHexCoords axialHexCoords = AxialHexCoords.FromXZ(transformPosition.x, transformPosition.z);
        return hexMap.RemoveTile(axialHexCoords);
    }
}
}