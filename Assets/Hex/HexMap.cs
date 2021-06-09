using System;
using System.Collections.Generic;
using System.Linq;
using Hex;
using UnityEngine;

public class HexMap : MonoBehaviour
{

    public static HexMap instance;
    public TileSO voidTileSo;
    
    [Serializable]
    public class Slot
    {
        public AxialHexCoords Coords;
        public Tile Tile;

        public Slot(AxialHexCoords coords, Tile tile)
        {
            Coords = coords;
            Tile = tile;
        }
    }

    public List<Slot> slots = new();
    public List<Slot> voids = new();

    private void Awake()
    {
        instance = this;
    }

    public Tile SetHexTile(AxialHexCoords coords, int angle, TileSO tileSo)
    {
        Tile oldSlot = TileAt(coords);
        
        if (oldSlot != null) return null;

        Tile placedTile = PlaceTile(coords, angle, tileSo);
        NotifyNeighbours(coords);
        UpdateVoidBorder();
        return placedTile;
    }
    private Tile PlaceTile(AxialHexCoords coords, int angle, TileSO value)
    {

        Vector3 axialToWorld = coords.ToWorldVector3();
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        var tile = Instantiate(value.asset, axialToWorld, rotation).GetComponent<Tile>();
        var slot = new Slot(coords, tile);
        slots.Add(slot);
        
        List<Tile> neighbours = coords.Neighbours().Select(TileAt).Where(neighbour => neighbour).ToList();
        tile.OnNeighboursChanged(neighbours);
        foreach (TileBehaviour tileLifecycleAware in tile.GetComponents<TileBehaviour>())
        {
            tileLifecycleAware.OnTilePlaced(neighbours);
        }

        return tile;
    }

    public List<Tile> TileNeighbours(Tile tile)
    {
        AxialHexCoords oldCoords = slots.Find(slot => slot.Tile == tile).Coords;
        return oldCoords.Neighbours().Select(TileAt).Where(it => it != null).ToList();
    }
    
    private void NotifyNeighbours(AxialHexCoords coords)
    {
        foreach (AxialHexCoords neighbour in coords.Neighbours())
        {
            Tile neighbourTile = TileAt(neighbour);
            neighbourTile?.OnNeighboursChanged(neighbour.Neighbours().Select(TileAt).ToList());
        }
    }
    
    public void ForceRemoveTile(Tile tile)
    {
        Slot old = slots.Find(slot => slot.Tile == tile);
        slots.Remove(old);
        Destroy(tile.gameObject);
        NotifyNeighbours(old.Coords);
        UpdateVoidBorder();
    }
    
    public bool RemoveTile(AxialHexCoords coords)
    {
        Slot old = slots.Find(slot => Equals(slot.Coords, coords));
        if (old == null) return false;

        Tile oldTile = old.Tile;
        if (oldTile.isIrreplaceable) return false;
        
        slots.Remove(old);
        
        foreach (TileBehaviour tileLifecycleAware in oldTile.GetComponents<TileBehaviour>())
        {
            tileLifecycleAware.OnTileRemoved();
        }
        NotifyNeighbours(old.Coords);
        UpdateVoidBorder();
        oldTile.Drop();
        return true;
    }
    
    public void ReplaceTile(Tile tile, TileSO tileSo)
    {
        Slot old = slots.Find(slot => slot.Tile == tile);
        slots.Remove(old);
        Destroy(tile.gameObject);
        
        PlaceTile(old.Coords, 0, tileSo);
        NotifyNeighbours(old.Coords);
        UpdateVoidBorder();
    }
    
    private Tile TileAt(AxialHexCoords coords)
    {

        return slots.FirstOrDefault(it => it.Coords.Equals(coords))?.Tile;
    }

    private void Start()
    {
        UpdateVoidBorder();
    }
    public void UpdateVoidBorder()
    {
        IEnumerable<AxialHexCoords> axialHexCoordsEnumerable =
            slots.Select(it => it.Coords)
                .SelectMany(it => it.Neighbours())
                .Distinct()
                .Where(it => slots.All(slot => !Equals(slot.Coords, it)))
                .ToList();
        
        foreach (Slot @void in voids)
        {
            Destroy(@void.Tile.gameObject);
        }
        voids.Clear();
        foreach (AxialHexCoords axialHexCoords in axialHexCoordsEnumerable)
        {
            GameObject newTile = Instantiate(voidTileSo.asset, axialHexCoords.ToWorldVector3(), Quaternion.identity);
            var slot = new Slot(axialHexCoords, newTile.GetComponent<Tile>());
            voids.Add(slot);
        }
    }
}