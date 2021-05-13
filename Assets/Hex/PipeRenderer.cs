using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Pipe))]
[ExecuteAlways]
public class PipeRenderer : MonoBehaviour
{
    public int segments = 10;
    public float width = 0.2f;

    private Pipe _pipe;
    private MeshFilter _meshFilter;

    private void Awake()
    {
        _pipe = GetComponent<Pipe>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        var path = BuildPathBetween(_pipe.endpoints[0], _pipe.endpoints[1], _pipe.height);
        BuildMesh(path);
    }

    private Path BuildPathBetween(OrientedPoint a, OrientedPoint b, int height)
    {
        var path = new Path();
        var handleWeight = Math.Max(.45f * (a.position - b.position).magnitude, 0.23f);
        path.points = new Vector3[]
        {
            a.position,
            a.LocalToWorld(handleWeight * Vector3.forward) + Vector3.up * height * width * 2,
            b.LocalToWorld(handleWeight * Vector3.forward) + Vector3.up * height * width * 2,
            b.position
        };
        return path;
    }

    private void BuildMesh(Path path)
    {
        var bevelVertices = ObtainBevelVertices();
        var mesh = new Mesh();

        var vertices = new Vector3[bevelVertices.Length * (segments + 1)];
        var normals = new Vector3[bevelVertices.Length * (segments + 1)];
        var uv = new Vector2[bevelVertices.Length * (segments + 1)];
        var triangles = new int[bevelVertices.Length * segments * 6];
        var step = 1f / segments;
        var vertexIndex = 0;
        for (var i = 0; i <= segments; i++)
        {
            var t = i * step;
            // obtain position & tangent in path
            var (position, tangent) = path.GetPosition(t);
            // add each bevelVertex, rotated and positioned
            for (var bevelIndex = 0; bevelIndex < bevelVertices.Length; bevelIndex++)
            {
                vertices[vertexIndex] = Quaternion.LookRotation(tangent) * bevelVertices[bevelIndex] + position;
                normals[vertexIndex] = Quaternion.LookRotation(tangent) * bevelVertices[bevelIndex].normalized;
                uv[vertexIndex] = new Vector2(bevelIndex, 0);
                vertexIndex++;
            }
        }

        var triangleIndex = 0;
        var bevelCount = bevelVertices.Length;
        for (int i = 0; i < segments * bevelCount; i++)
        {
            var middle = (i+1) % bevelCount != 0;
            //FIXME: Fix this shit
            triangles[triangleIndex + 0] = i;
            triangles[triangleIndex + 1] = i + bevelCount;
            triangles[triangleIndex + 2] = middle ? (i + 1) : (i/bevelCount)*bevelCount;

            triangles[triangleIndex + 3] = middle ? (i + 1) : (i/bevelCount)*bevelCount;
            triangles[triangleIndex + 4] = i + bevelCount;
            triangles[triangleIndex + 5] = middle ? (i + bevelCount + 1) : i + 1;

            triangleIndex += 6;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;
        _meshFilter.mesh = mesh;
    }

    private Vector3[] ObtainBevelVertices()
    {
        var radius = (width * .5f);
        var stripBevels = new[]
        {
            Vector3.left * radius + .01f * Vector3.up,
            Vector3.right * radius + .01f * Vector3.up
        };

        var amount = 16;
        var circleBevels = new Vector3[amount];
        for (var i = 0; i < amount; i++)
        {
            circleBevels[i] = Quaternion.AngleAxis(360f * -i / amount, Vector3.forward) * Vector3.down * radius;
        }

        return circleBevels;
    }
}