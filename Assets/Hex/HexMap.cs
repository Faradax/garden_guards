using System;
using System.Collections.Generic;
using System.Linq;
using Hex;
using UnityEditor;
using UnityEngine;

public class HexMap : MonoBehaviour
{

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

    public void SetHexTile(AxialHexCoords coords, TileSO value)
    {
        Slot oldSlot = slots.FirstOrDefault(it => it.Coords.Equals(coords));
        if (oldSlot != null)
        {
            Destroy(oldSlot.Tile.gameObject);
        }
        Vector3 axialToWorld = coords.ToWorldVector3();
        GameObject newTile = Instantiate(value.asset, axialToWorld, Quaternion.identity);
        var slot = new Slot(coords, newTile.GetComponent<Tile>());
        slots.Add(slot);
        UpdateVoidBorder();
    }

    private void Start()
    {
        UpdateVoidBorder();
    }
    public void UpdateVoidBorder()
    {
        Debug.Log("Updating World Border");
        var voidTileSo = AssetDatabase.LoadAssetAtPath<TileSO>("Assets/Hex/Tiles/Void/VoidTile.asset");
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
            GameObject newTile = Instantiate(voidTileSo.asset, axialHexCoords.ToWorldVector3(), Quaternion.identity);
            var slot = new Slot(axialHexCoords, newTile.GetComponent<Tile>());
            voids.Add(slot);
        }
    }
}