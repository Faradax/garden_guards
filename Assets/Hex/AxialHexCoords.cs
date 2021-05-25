using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hex
{
[Serializable]
public class AxialHexCoords
{
    public int Q;
    public int R;
    public AxialHexCoords(int q, int r)
    {
        Q = q;
        R = r;
    }
    public static AxialHexCoords operator +(AxialHexCoords a) => a;
    public static AxialHexCoords operator -(AxialHexCoords a) => new(-a.Q, -a.R);
    
    public static AxialHexCoords operator +(AxialHexCoords a, AxialHexCoords b) => new(a.Q + b.Q, a.R + b.R);
    public static AxialHexCoords operator -(AxialHexCoords a, AxialHexCoords b) => new(a.Q - b.Q, a.R - b.R);
    
    public static AxialHexCoords FromXZ(float worldClickX, float worldClickZ)
    {
        Vector2 fractionalAxial = XYZToAxial(new Vector3(worldClickX, worldClickZ));
        Vector3 fractionalCube = AxialToCube(fractionalAxial);
        Vector3 roundedCube = CubeRound(fractionalCube);
        Vector2 roundedAxial = CubeToAxial(roundedCube);
        
        return new AxialHexCoords((int) roundedAxial.x, (int) roundedAxial.y);
    }

    private static Vector2 XYZToAxial(Vector2 worldXZ)
    {

        float q = (Mathf.Sqrt(3) / 3 * worldXZ.x - 1f / 3 * worldXZ.y) / 1;
        float r = (2f / 3 * worldXZ.y) / 1;
        return new Vector2(q, r);
    }
    
    public Vector3 ToWorldVector3()
    {
        float x = 1 * (Mathf.Sqrt(3) * Q + Mathf.Sqrt(3) / 2 * R);
        float y = 1 * (3f / 2 * R);
        return new Vector3(x, 0,  y);
    }

    private static Vector3 AxialToCube(Vector2 hex)
    {
        float x = hex.x;
        float z = hex.y;
        float y = -x - z;
        return new Vector3(x, y, z);
    }
    private static Vector3 CubeRound(Vector3 cube)
    {
        int rx = Mathf.RoundToInt(cube.x);
        int ry = Mathf.RoundToInt(cube.y);
        int rz = Mathf.RoundToInt(cube.z);

        float xDiff = Mathf.Abs(rx - cube.x);
        float yDiff = Mathf.Abs(ry - cube.y);
        float zDiff = Mathf.Abs(rz - cube.z);

        if (xDiff > yDiff && xDiff > zDiff)
        {
            rx = -ry - rz;
        }
        else if (yDiff > zDiff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }

        return new Vector3Int(rx, ry, rz);
    }
    private static Vector2 CubeToAxial(Vector3 cube)
    {
        float q = cube.x;
        float r = cube.z;
        return new Vector2(q, r);
    }

    public List<AxialHexCoords> Neighbours()
    {
        AxialHexCoords[] axialDirections =
        {
            new(+1, 0), new(+1, -1), new(0, -1),
            new(-1, 0), new(-1, +1), new(0, +1)
        };
        return axialDirections.Select(offset => this + offset).ToList();
    }

    protected bool Equals(AxialHexCoords other)
    {
        return Q == other.Q && R == other.R;
    }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AxialHexCoords) obj);
    }
    public override int GetHashCode()
    {
        unchecked
        {
            return (Q * 397) ^ R;
        }
    }
}
}