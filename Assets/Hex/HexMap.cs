using UnityEngine;

public class HexMap: MonoBehaviour
{

    public static Vector2 WorldToAxial(Vector3 vector3)
    {

        float q = (Mathf.Sqrt(3) / 3 * vector3.x - 1f / 3 * vector3.z) / 1;
        float r = (2f / 3 * vector3.z) / 1;
        return new Vector2(q, r);
    }
    public static Vector3 CubeRound(Vector3 cube)
    {
        var rx = Mathf.Round(cube.x);
        var ry = Mathf.Round(cube.y);
        var rz = Mathf.Round(cube.z);

        var x_diff = Mathf.Abs(rx - cube.x);
        var y_diff = Mathf.Abs(ry - cube.y);
        var z_diff = Mathf.Abs(rz - cube.z);

        if (x_diff > y_diff && x_diff > z_diff)
        {
            rx = -ry - rz;
        }
        else if (y_diff > z_diff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }

        return new(rx, ry, rz);
    }
    public static Vector2 AxialToWorld(Vector2 hex)
    {
        var x = 1 * (Mathf.Sqrt(3) * hex.x + Mathf.Sqrt(3) / 2 * hex.y);
        var y = 1 * (3f / 2 * hex.y);
        return new Vector2(x, y);
    }
    public static Vector2 CubeToAxial(Vector3 cube)
    {
        var q = cube.x;
        var r = cube.z;
        return new Vector2(q, r);
    }
    public static Vector3 AxialToCube(Vector2 hex)
    {
        var x = hex.x;
        var z = hex.y;

        var y = -x - z;
        return new Vector3(x, y, z);
    }
}