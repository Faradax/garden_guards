using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    
    private List<SphereCollider> endpointColliders = new List<SphereCollider>();
    private Color _color;
    private MeshRenderer _meshRenderer;
    private bool _hasMeshRenderer;
    
    [SerializeField]
    public List<OrientedPoint> endpoints;
    public int height;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _hasMeshRenderer = _meshRenderer != null;
    }

    public void Init(List<OrientedPoint> endpoints, int height)
    {
        foreach (var orientedPoint in endpoints)
        {
            var sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.center = orientedPoint.position;
            sphereCollider.radius = .1f;
            endpointColliders.Add(sphereCollider);
        }

        this.endpoints = endpoints;
        this.height = height;
    }

    public void Fill(Color color)
    {
        if (_color == color) return;

        if (_hasMeshRenderer)
        {
            _color = color;
            var material = _meshRenderer.material;
            material.color = color;
            _meshRenderer.material = material;
        }

        FindPipeNeighbours().ForEach(pipe => pipe.Fill(color));
    }

    private List<Pipe> FindPipeNeighbours()
    {
        // sample each endpointCollider
        var pipes = endpointColliders.SelectMany(endpointCollider =>
            {
                var position = endpointCollider.center + endpointCollider.transform.position;
                var radius = endpointCollider.radius;
                var results = new Collider[5];
                var size = Physics.OverlapSphereNonAlloc(position, radius, results);
                return results.Take(size);
            })
            .Select(it => it.gameObject)
            .Select(it => it.GetComponent<Pipe>())
            .Except(new []{this,null}).ToList();
        return pipes;
    }
}