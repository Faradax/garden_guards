using System;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;

public class VectorMapper : MonoBehaviour, ISerializationCallbackReceiver
{

    public float area;
    public int subdivisions = 8;

    public List<PathCreator> pathCreators;

    public Vector3[,] vectors = { };
    public float maxDistanceToPath = 3;
    public float forwardWeightPower = 4;

    [SerializeField]
    [HideInInspector]
    private Vector3[] serializedVectors;
    
    public void MapVectorField()
    {
        Vector3 offset = new Vector3(-area / 2, 0, -area / 2);
        vectors = new Vector3[subdivisions, subdivisions];

        for (var i = 0; i < subdivisions; i++)
        {
            for (var j = 0; j < subdivisions; j++)
            {
                VertexPath firstPath = pathCreators.First().path;
                Vector3 samplePoint = offset + new Vector3(area * i / subdivisions, 0, area * j / subdivisions);
                float timeOnPath = firstPath.GetClosestTimeOnPath(
                    samplePoint);
                Vector3 pointOnPath = firstPath.GetPointAtTime(timeOnPath);
                Vector3 toPath = (pointOnPath) / 1 - samplePoint;

                float distanceToPath = toPath.magnitude;
                if (distanceToPath < maxDistanceToPath)
                {
                    float farness = distanceToPath / maxDistanceToPath;
                    float closeness = 1 - farness;
                    Vector3 forward = firstPath.GetDirection(timeOnPath);
                    float forwardWeight = Mathf.Pow(closeness, forwardWeightPower);
                    toPath = Vector3.Lerp(toPath.normalized, forward, forwardWeight);
                }

                vectors[i, j] = toPath.normalized;
            }
        }
    }
    public Vector3 Sample(Vector3 currentPosition)
    {
        Vector3 center = transform.position;
        float leftBorder = center.x - area / 2;
        float rightBorder = center.x + area / 2;
        float topBorder = center.z - area / 2;
        float bottomBorder = center.z + area / 2;
        float xRelative = Mathf.Clamp01(Mathf.InverseLerp(leftBorder, rightBorder, currentPosition.x));
        float zRelative = Mathf.Clamp01(Mathf.InverseLerp(topBorder, bottomBorder, currentPosition.z));

        float floatXIndex = xRelative * subdivisions;
        int xIndex = (int) floatXIndex;
        float floatZIndex = zRelative * subdivisions;
        int zIndex = (int) floatZIndex;

        float xWeight = floatXIndex - xIndex;
        float zWeight = floatZIndex - zIndex;
        
        Vector3 q0 = vectors[xIndex, zIndex];
        if (xIndex == subdivisions - 1 || zIndex == subdivisions - 1)
        {
            return q0;
        }
        Vector3 q1 = vectors[xIndex+1, zIndex];
        Vector3 q2 = vectors[xIndex, zIndex+1];
        Vector3 q3 = vectors[xIndex+1, zIndex+1];

        Vector3 r0 = Vector3.Lerp(q0, q1, xWeight);
        Vector3 r1 = Vector3.Lerp(q2, q3, xWeight);
        Vector3 p = Vector3.Lerp(r0, r1, zWeight);
        return p;
    }

    #region Serialization

    public void OnBeforeSerialize()
    {
        serializedVectors = new Vector3[subdivisions * subdivisions];
        for (var i = 0; i < serializedVectors.Length; i++)
        {
            int x = i % subdivisions;
            int z = i / subdivisions;
            serializedVectors[i] = vectors[x, z];
        }
    }
    public void OnAfterDeserialize()
    {
        vectors = new Vector3[subdivisions, subdivisions];
        for (var i = 0; i < serializedVectors.Length; i++)
        {
            int x = i % subdivisions;
            int z = i / subdivisions;
            vectors[x, z] = serializedVectors[i];
        }
    }

    #endregion

    public void ApplyMouse(Vector3 mouseInField, Vector3 mouseDeltaNormalized)
    {
        Vector3 center = transform.position;
        float leftBorder = center.x - area / 2;
        float rightBorder = center.x + area / 2;
        float topBorder = center.z - area / 2;
        float bottomBorder = center.z + area / 2;
        float xRelative = Mathf.Clamp01(Mathf.InverseLerp(leftBorder, rightBorder, mouseInField.x));
        float zRelative = Mathf.Clamp01(Mathf.InverseLerp(topBorder, bottomBorder, mouseInField.z));

        float floatXIndex = xRelative * subdivisions;
        int xIndex = (int) floatXIndex;
        float floatZIndex = zRelative * subdivisions;
        int zIndex = (int) floatZIndex;

        int xmin = Math.Max(0, xIndex - 3);
        int zmin = Math.Max(0, zIndex - 3);
        int xmax = Math.Min(subdivisions-1, xIndex + 3);
        int zmax = Math.Min(subdivisions-1, zIndex + 3);
        for (int i = xmin; i <= xmax; i++)
        {
            for (int j = zmin; j <= zmax; j++)
            {
                float distanceToCenter = (new Vector2(i, j) - new Vector2(xIndex, zIndex)).magnitude/3;
                distanceToCenter = Mathf.Clamp01(distanceToCenter);
                vectors[i, j] = Vector3.Lerp(vectors[i, j], mouseDeltaNormalized, 1-distanceToCenter);
            }    
        }


    }
}