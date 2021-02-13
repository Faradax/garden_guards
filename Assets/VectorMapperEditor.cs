using PathCreation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VectorMapper))]
public class VectorMapperEditor : Editor
{
    private VectorMapper _vectorMapper;
    private Vector3 _mouseInField;
    private Vector3 _oldMouseInField;
    private bool _editing;

    private void OnEnable()
    {
        _vectorMapper = (VectorMapper) target;
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "VectorMapper changed");
        _vectorMapper.pathCreators[0] =
            (PathCreator) EditorGUILayout.ObjectField(_vectorMapper.pathCreators[0], typeof(PathCreator), true);
        _vectorMapper.area = EditorGUILayout.FloatField("Area", _vectorMapper.area);
        _vectorMapper.subdivisions = EditorGUILayout.IntSlider("Subdivisions", _vectorMapper.subdivisions, 4, 100);
        _vectorMapper.maxDistanceToPath = EditorGUILayout.FloatField("Max Distance to Path", _vectorMapper.maxDistanceToPath);
        _vectorMapper.forwardWeightPower = EditorGUILayout.FloatField("Forward Weight Power", _vectorMapper.forwardWeightPower);
        _editing = EditorGUILayout.Toggle("Edit mode", _editing);
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
        for (var i = 0; i < mapper.vectors.GetLength(0); i++)
        {
            for (var j = 0; j < mapper.vectors.GetLength(1); j++)
            {
                Vector3 start = offset + new Vector3(i * stepSize, 0, j * stepSize) + mapper.transform.position;
                Handles.DrawLine(start, start + mapper.vectors[i, j] * .4f);
            }
        }
        Handles.DrawWireCube(mapper.transform.position, new Vector3(mapper.area, 1, mapper.area));

        if (_editing)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            var plane = new Plane(_vectorMapper.transform.up, _vectorMapper.transform.position);
            float distance;
            bool raycast = plane.Raycast(ray, out distance);
            if (raycast)
            {
                Vector3 oldMouseInField = ray.GetPoint(distance);
                Handles.DrawWireArc(oldMouseInField, Vector3.up, Vector3.forward, 360, 1);
                if (Event.current.type == EventType.MouseDrag)
                {
                    _mouseInField = oldMouseInField;
                    Vector3 mouseDelta = _mouseInField - _oldMouseInField;
                    if (mouseDelta.magnitude > 0)
                    {
                        _vectorMapper.ApplyMouse(_mouseInField, mouseDelta.normalized);
                    }
                    _oldMouseInField = oldMouseInField;
                }
                Repaint();
                EditorApplication.QueuePlayerLoopUpdate ();
            }
            // Don't allow clicking over empty space to deselect the object
            if (Event.current.type == EventType.Layout)
            {
                HandleUtility.AddDefaultControl(0);
            }
        }
    }
}