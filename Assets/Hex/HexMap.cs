using System;
using System.Collections.Generic;
using System.Linq;
using Hex;
using UnityEngine;

public class HexMap : MonoBehaviour
{

    [Serializable]
    public record Slot(AxialHexCoords Coords, Tile Tile);

    public List<Slot> slots = new();

    public void SetHexTile(AxialHexCoords coords, TileSO value)
    {
        Slot oldSlot = slots.FirstOrDefault(it => it.Coords.Equals(coords));
        if (oldSlot != null)
        {
            Destroy(oldSlot.Tile.gameObject);
        }

        List<AxialHexCoords> neighbours = coords.Neighbours();
        foreach (var axialHexCoords in neighbours)
        {
            Debug.Log(axialHexCoords);
        }
        Vector3 axialToWorld = coords.ToWorldVector3();
        GameObject newTile = Instantiate(value.asset, axialToWorld, Quaternion.identity);
        var slot = new Slot(coords, newTile.GetComponent<Tile>());
        slots.Add(slot);
    }
}