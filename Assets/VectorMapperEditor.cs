using System;
using PathCreation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VectorMapper))]
public class VectorMapperEditor : Editor
{
    private VectorMapper _vectorMapper;

    private void OnEnable()
    {
        _vectorMapper = (VectorMapper) target;
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "VectorMapper changed");
        _vectorMapper.pathCreators[0] = (PathCreator) EditorGUILayout.ObjectField(_vectorMapper.pathCreators[0], typeof(PathCreator), true);
        _vectorMapper.area = EditorGUILayout.FloatField("Area", _vectorMapper.area);
        _vectorMapper.subdivisions = EditorGUILayout.IntSlider("Subdivisions", _vectorMapper.subdivisions, 4, 100);
        _vectorMapper.maxDistanceToPath = EditorGUILayout.FloatField("Max Distance to Path", _vectorMapper.maxDistanceToPath);
        _vectorMapper.forwardWeightPower = EditorGUILayout.FloatField("Forward Weight Power", _vectorMapper.forwardWeightPower);
        if (GUILayout.Button("Calculate"))
        {
            _vectorMapper.MapVectorField();
        }      
}
    
    private void OnSceneGUI()
    {
        VectorMapper mapper = _vectorMapper;
        Vector3 offset = new Vector3(-mapper.area / 2, 0, -mapper.area / 2);
        float stepSize = mapper.area / mapper.subdivisions;
        for (var i = 0; i < mapper.vectors.Length; i++)
        {
            int x = i % mapper.subdivisions;
            int z = i / mapper.subdivisions;
            Vector3 start = offset + new Vector3(x * stepSize, 0, z * stepSize) + mapper.transform.position;
            Handles.DrawLine(start, start + mapper.vectors[i]*.4f);
        }
        Handles.DrawWireCube(mapper.transform.position, new Vector3(mapper.area, 1, mapper.area));
    }
}