using UnityEngine;

namespace Hex
{
public class PlaceNewTileFlow : MonoBehaviour, IInteraction
{
    public HexMap hexMap;
    public TowerEvent towerPlacedEvent;

    private TileSO _tileToPlace;

    public void PlaceTile(TileSO tileToPlace)
    {
        _tileToPlace = tileToPlace;
    }

    public void OnTileClicked(Clickable target)
    {
        PlaceTile(target);
    }
    private void PlaceTile(Clickable target)
    {
        Vector3 transformPosition = target.transform.position;
        AxialHexCoords axialHexCoords = AxialHexCoords.FromXZ(transformPosition.x, transformPosition.z);
        Tile placedTile = hexMap.SetHexTile(axialHexCoords, _tileToPlace);

        if (!placedTile) return;

        towerPlacedEvent.Invoke(_tileToPlace);
        _tileToPlace = null;
    }

    public void Abort()
    {
        _tileToPlace = null;
    }
}
}