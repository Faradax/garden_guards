using System;
using UnityEngine;

[Serializable]
public class Path
{

    public Vector3[] points = new Vector3[4];

    public (Vector3, Vector3) GetPosition(float t)
    {
        var a = points[0];
        var b = points[1];
        var c = points[2];
        var d = points[3];

        var e = Vector3.Lerp(a, b, t);
        var f = Vector3.Lerp(b, c, t);
        var g = Vector3.Lerp(c, d, t);
        
        var h = Vector3.Lerp(e, f, t);
        var i = Vector3.Lerp(f, g, t);
        
        var j = Vector3.Lerp(h, i, t);

        return (j, i-h);
    }
}