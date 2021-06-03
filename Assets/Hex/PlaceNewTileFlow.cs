using UnityEngine;

namespace Hex
{
public class PlaceNewTileFlow : MonoBehaviour
{
    public HexMap hexMap;
    public TowerEvent towerPlacedEvent;

    [HideInInspector]
    public bool active;

    private TileSO _tileToPlace;

    public void PlaceTile(TileSO tileToPlace)
    {
        _tileToPlace = tileToPlace;
        active = true;
    }

    public void OnTileClicked(Clickable target)
    {
        if (!active)
        {
            // TODO: Model interaction state properly 
            target.GetComponent<Tile>()?.OnClick();
            return;
        }
        PlaceTile(target);
    }
    private void PlaceTile(Clickable target)
    {
        Vector3 transformPosition = target.transform.position;
        AxialHexCoords axialHexCoords = AxialHexCoords.FromXZ(transformPosition.x, transformPosition.z);
        Tile placedTile = hexMap.SetHexTile(axialHexCoords, _tileToPlace);

        if (!placedTile) return;

        foreach (ITileLifecycleAware tileLifecycleAware in placedTile.GetComponents<ITileLifecycleAware>())
        {
            tileLifecycleAware.OnTilePlaced();
        }
        
        towerPlacedEvent.Invoke(_tileToPlace);
        active = false;
        _tileToPlace = null;
    }

    public void Abort()
    {
        active = false;
        _tileToPlace = null;
    }
}
}