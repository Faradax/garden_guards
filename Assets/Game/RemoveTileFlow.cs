using UnityEngine;

namespace Hex
{
public class RemoveTileFlow : MonoBehaviour, IInteraction
{
    public HexMap hexMap;

    public void OnTileClicked(Clickable target)
    {
        RemoveTile(target);
    }
    private void RemoveTile(Clickable target)
    {
        Vector3 transformPosition = target.transform.position;
        AxialHexCoords axialHexCoords = AxialHexCoords.FromXZ(transformPosition.x, transformPosition.z);
        hexMap.RemoveTile(axialHexCoords);
    }
}
}