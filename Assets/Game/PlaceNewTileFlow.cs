using UnityEngine;

namespace Hex
{
public class PlaceNewTileFlow : MonoBehaviour, IInteraction
{
    public HexMap hexMap;
    public TowerEvent towerPlacedEvent;
    public PreviewController previewController;
    
    private TileSO _tileToPlace;
    private int _angle;
    
    public void PlaceTile(TileSO tileToPlace)
    {
        _tileToPlace = tileToPlace;
        previewController.Show(tileToPlace);
        previewController.UpdateAngle(_angle);
    }

    public bool OnTileClicked(Clickable target)
    {
        return PlaceTile(target);
    }
    
    public void OnTileHovered(Tile tile)
    {
        previewController.UpdateTarget(tile);        
    }
    
    public void OnMouseWheel(int diff)
    {
        _angle = (_angle + diff * 60) % 360;
        previewController.UpdateAngle(_angle);
    }
    private bool PlaceTile(Clickable target)
    {
        Vector3 transformPosition = target.transform.position;
        AxialHexCoords axialHexCoords = AxialHexCoords.FromXZ(transformPosition.x, transformPosition.z);
        Tile placedTile = hexMap.SetHexTile(axialHexCoords, _angle, _tileToPlace);

        if (!placedTile) return false;

        towerPlacedEvent.Invoke(_tileToPlace);
        _tileToPlace = null;
        previewController.End();
        return true;
    }

    public void Abort()
    {
        _tileToPlace = null;
        previewController.End();
    }
}
}