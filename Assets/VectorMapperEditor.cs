using System;
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
        var o = new SerializedObject(target);
        Undo.RecordObject(target, "VectorMapper changed");
        _vectorMapper.subdivisions = EditorGUILayout.IntSlider(_vectorMapper.subdivisions, 4, 100);
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