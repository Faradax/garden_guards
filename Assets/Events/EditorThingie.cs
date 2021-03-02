using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class EditorThingie : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Invoke"))
        {
            ((GameEvent) target).Invoke();
        }
    }
}
