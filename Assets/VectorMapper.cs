using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;

public class VectorMapper : MonoBehaviour
{

    public float area;
    public int subdivisions = 8;
    
    public List<PathCreator> pathCreators;

    public Vector3[] vectors = {};
    public int distanceToPath = 3;

    void Start()
    {
        
    }

    private void OnValidate()
    {
        MapVectorField();
    }

    public void MapVectorField()
    {
        Vector3 offset = new Vector3(-area / 2, 0, -area / 2);
        vectors = new Vector3[subdivisions * subdivisions];
        
        for (var i = 0; i < subdivisions; i++)
        {
            for (var j = 0; j < subdivisions; j++)
            {
                VertexPath firstPath = pathCreators.First().path;
                Vector3 samplePoint = offset + new Vector3(area * i / subdivisions, 0, area * j / subdivisions);
                float timeOnPath = firstPath.GetClosestTimeOnPath(
                    samplePoint);
                Vector3 pointOnPath = firstPath.GetPointAtTime(timeOnPath);
                Vector3 toPath = pointOnPath - samplePoint;

                float distanceToPath = toPath.magnitude;
                if (distanceToPath < this.distanceToPath)
                {
                    float forwardWeight = 1 - distanceToPath / this.distanceToPath;
                    Vector3 forward = firstPath.GetDirection(timeOnPath);
                    toPath = Vector3.Lerp(toPath.normalized, forward, forwardWeight);
                }
                
                vectors[i + j * subdivisions] = toPath.normalized;
            }
        }
    }
    public Vector3 Sample(Vector3 currentPosition)
    {
        Vector3 center = transform.position;
        float leftBorder = center.x - area/2;
        float rightBorder = center.x + area/2;
        float topBorder = center.z - area/2;
        float bottomBorder = center.z + area/2;
        float xRelative = Mathf.Clamp01(Mathf.InverseLerp(leftBorder, rightBorder, currentPosition.x));
        float zRelative = Mathf.Clamp01(Mathf.InverseLerp(topBorder, bottomBorder, currentPosition.z));

        int xIndex = (int) (xRelative * subdivisions);
        int zIndex = (int) (zRelative * subdivisions) * subdivisions;
        int index = Math.Min(vectors.Length-1, xIndex + zIndex);
        return vectors[index];
    }
}
