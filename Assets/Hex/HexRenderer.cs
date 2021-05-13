using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class HexRenderer : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        PrepareMesh();
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        var meshCollider = GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }
    }

    private void PrepareMesh()
    {
        var mesh = new Mesh();

        var vertices = new Vector3[]
        {
            Vector3.zero,
            Quaternion.AngleAxis(30, Vector3.up) * Vector3.forward,
            Quaternion.AngleAxis(90, Vector3.up) * Vector3.forward,
            Quaternion.AngleAxis(150, Vector3.up) * Vector3.forward,
            Quaternion.AngleAxis(210, Vector3.up) * Vector3.forward,
            Quaternion.AngleAxis(270, Vector3.up) * Vector3.forward,
            Quaternion.AngleAxis(330, Vector3.up) * Vector3.forward
        };

        var triangles = new int[]
        {
            1, 2, 0,
            2, 3, 0,
            3, 4, 0,
            4, 5, 0,
            5, 6, 0,
            6, 1, 0,
        };

        var normals = new Vector3[]
        {
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up
        };
        var uv = new Vector2[]
        {
            new Vector2(.5f, 1),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;
        _meshFilter.mesh = mesh;
        
        for (var i = 1; i < vertices.Length; i++)
        {
            var a = vertices[i];
            var b = i == vertices.Length - 1 ? vertices[1] : vertices[i + 1];
            var edgeCenter = (a + b) * 0.5f;
        }
    }
}