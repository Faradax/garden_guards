using System;
using System.Collections.Generic;
using System.Linq;
using Hex;
using UnityEditor;
using UnityEngine;

public class HexMap : MonoBehaviour
{

    public static HexMap instance;
    
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
    private TileSO _voidTileSo;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        _voidTileSo = AssetDatabase.LoadAssetAtPath<TileSO>("Assets/Hex/Tiles/Void/VoidTile.asset");
    }

    public Tile SetHexTile(AxialHexCoords coords, TileSO tileSo)
    {
        Tile oldSlot = TileAt(coords);
        
        if (oldSlot != null) return null;

        Tile placedTile = PlaceTile(coords, tileSo);
        NotifyNeighbours(coords);
        UpdateVoidBorder();
        return placedTile;
    }
    private Tile PlaceTile(AxialHexCoords coords, TileSO value)
    {

        Vector3 axialToWorld = coords.ToWorldVector3();
        var tile = Instantiate(value.asset, axialToWorld, Quaternion.identity).GetComponent<Tile>();
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
    private void NotifyNeighbours(AxialHexCoords coords)
    {
        foreach (AxialHexCoords neighbour in coords.Neighbours())
        {
            Tile neighbourTile = TileAt(neighbour);
            neighbourTile?.OnNeighboursChanged(neighbour.Neighbours().Select(TileAt).ToList());
        }
    }
    
    public void RemoveTile(Tile tile)
    {
        Slot old = slots.Find(slot => slot.Tile == tile);
        Destroy(tile.gameObject);
        slots.Remove(old);
        NotifyNeighbours(old.Coords);
        UpdateVoidBorder();
    }
    
    public bool RemoveTile(AxialHexCoords coords)
    {
        Slot old = slots.Find(slot => Equals(slot.Coords, coords));
        if (old == null) return false;
        
        Destroy(old.Tile.gameObject);
        slots.Remove(old);
        NotifyNeighbours(old.Coords);
        UpdateVoidBorder();
        return true;
    }
    
    public void ReplaceTile(Tile tile, TileSO tileSo)
    {
        Slot old = slots.Find(slot => slot.Tile == tile);
        Destroy(tile.gameObject);
        
        PlaceTile(old.Coords, tileSo);
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
            slots.Where(slot => !slot.Tile.isEmpty())
                .Select(it => it.Coords)
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
            GameObject newTile = Instantiate(_voidTileSo.asset, axialHexCoords.ToWorldVector3(), Quaternion.identity);
            var slot = new Slot(axialHexCoords, newTile.GetComponent<Tile>());
            voids.Add(slot);
        }
    }
}