using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    private static float radius = 1;
    private static float innerRadius = 0.866025404f * radius;

    [HideInInspector] public List<OrientedPoint> availableEdges = new List<OrientedPoint>();
    private int _sides = 6;

    private List<List<OrientedPoint>> freePathsBetween = new List<List<OrientedPoint>>();

    public Pipes()
    {
        Vector3 toEdge = Vector3.forward * innerRadius;
        for (int i = 0; i < _sides; i++)
        {
            var rotation = Quaternion.AngleAxis(60 * i, Vector3.up);
            var leftPoint = rotation * (toEdge + 0.2f * Vector3.left);
            var rightPoint = rotation * (toEdge + 0.2f * Vector3.right);
            var edgeNormal = Quaternion.AngleAxis(60 * i + 180, Vector3.up);

            availableEdges.Add(new OrientedPoint(leftPoint, edgeNormal));
            availableEdges.Add(new OrientedPoint(rightPoint, edgeNormal));
        }

        freePathsBetween.Add(new List<OrientedPoint>(availableEdges));
    }

    public void ConnectPipe(OrientedPoint a, OrientedPoint b)
    {
        var emptyPipe = new GameObject("Pipe");
        emptyPipe.transform.parent = transform;
        emptyPipe.transform.localPosition = Vector3.zero;

        // Semantic pipe
        var height = 1;
        if (freePathsBetween.Any(list => list.Contains(a) && list.Contains(b)))
        {
            height = 0;
        }
        var pipe = emptyPipe.AddComponent<Pipe>();
        pipe.Init(new List<OrientedPoint>() {a, b}, height);

        // Visual Pipe
        PipeRenderer pipeRenderer = emptyPipe.AddComponent<PipeRenderer>();

        UpdateFreePaths(a, b);
    }

    private void UpdateFreePaths(OrientedPoint a, OrientedPoint b)
    {
        var indexA = availableEdges.IndexOf(a);
        var indexB = availableEdges.IndexOf(b);
        if (indexA > indexB)
        {
            var swap = indexA;
            indexA = indexB;
            indexB = swap;
        }

        var pointsFirstHalf = new List<OrientedPoint>();
        var pointsSecondHalf = new List<OrientedPoint>();
        for (var i = 0; i < availableEdges.Count; i++)
        {
            var orientedPoint = availableEdges[i];

            if (i < indexA || i > indexB)
            {
                pointsFirstHalf.Add(orientedPoint);
            }
            else if (i > indexA && i < indexB)
            {
                pointsSecondHalf.Add(orientedPoint);
            }
        }

        var newFreePathsBetween = new List<List<OrientedPoint>>();
        foreach (var orientedPoints in freePathsBetween)
        {
            var firstHalfPart = orientedPoints.Intersect(pointsFirstHalf).ToList();
            var secondHalfPart = orientedPoints.Intersect(pointsSecondHalf).ToList();

            newFreePathsBetween.Add(firstHalfPart);
            newFreePathsBetween.Add(secondHalfPart);
        }

        freePathsBetween = newFreePathsBetween;
    }
}