using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pipes))]
public class PipesEditor : Editor
{
    private Pipes _pipes;
    private OrientedPoint _lastNode = null;
    private void OnEnable()
    {
        _pipes = (Pipes) target;
    }

    private void OnSceneGUI()
    {
        Draw();
    }
    
    
    void Draw()
    {
        for (var i = 0; i < _pipes.availableEdges.Count; i++)
        {
            var socket = _pipes.availableEdges[i];
            var edgeCenter = socket.position;
            var clicked = Handles.Button(_pipes.transform.position + edgeCenter, socket.rotation, .1f, .1f,
                Handles.CylinderHandleCap);
            if (clicked)
            {
                if (_lastNode != null)
                {
                    if (_lastNode != socket)
                    {
                        _pipes.ConnectPipe(_lastNode, socket);
                    }
                    _lastNode = null;
                }
                else
                {
                    _lastNode = socket;
                }
            }
        }
    }
}