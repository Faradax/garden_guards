using UnityEngine;

namespace Hex
{
public class PlaceNewTileFlow : MonoBehaviour, IInteraction
{
    public HexMap hexMap;
    public TowerEvent towerPlacedEvent;

    private TileSO _tileToPlace;

    private int _angle = 0;
    
    public void PlaceTile(TileSO tileToPlace)
    {
        _tileToPlace = tileToPlace;
    }

    public bool OnTileClicked(Clickable target)
    {
        return PlaceTile(target);
    }
    public void OnMouseWheel(int diff)
    {
        _angle = (_angle + diff * 60) % 360;
        Debug.Log(_angle);
    }
    private bool PlaceTile(Clickable target)
    {
        Vector3 transformPosition = target.transform.position;
        AxialHexCoords axialHexCoords = AxialHexCoords.FromXZ(transformPosition.x, transformPosition.z);
        Tile placedTile = hexMap.SetHexTile(axialHexCoords, _angle, _tileToPlace);

        if (!placedTile) return false;

        towerPlacedEvent.Invoke(_tileToPlace);
        _tileToPlace = null;
        return true;
    }

    public void Abort()
    {
        _tileToPlace = null;
    }
}
}