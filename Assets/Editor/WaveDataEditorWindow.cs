using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class WaveDataEditorWindow : EditorWindow
{

    internal static class Styles
    {
        public static GUIStyle lane = new GUIStyle("box")
        {
            fixedHeight = 60,
            alignment = TextAnchor.MiddleCenter,
            overflow = new RectOffset()
        };
        public static GUIStyle ruler = new GUIStyle("box")
        {
            fixedHeight = 20,
            alignment = TextAnchor.MiddleCenter,
            overflow = new RectOffset()
        };

        public static GUIStyle spawn = new GUIStyle()
        {
            normal =
            {
                background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Background_Spawn.png")
            },
            overflow = new RectOffset(-5,-5,-5,-5),
            padding = new RectOffset(5,5,5,5),
            alignment = TextAnchor.MiddleCenter
        };
    }
    
    public WaveData waveData;
    private bool _toggle;
    private Vector2 _scrollPosition;

    public static void Open(WaveData waveData)
    {
        var waveDataEditorWindow = GetWindow<WaveDataEditorWindow>();
        waveDataEditorWindow.waveData = waveData;
        waveDataEditorWindow.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        DrawWaveSelectorView();
        DrawWaveEditorView();
        EditorGUILayout.EndHorizontal();
    }
    private void DrawWaveSelectorView()
    {
        // Nop
    }
    private void DrawWaveEditorView()
    {
        EditorGUILayout.BeginVertical();
        DrawToolbar();
        DrawLaneView();
        EditorGUILayout.EndVertical();
    }
    private void DrawToolbar()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        _toggle = EditorGUILayout.Toggle(EditorGUIUtility.TrIconContent("GridAxisX", "Snap to X"),
            _toggle,
            EditorStyles.toolbarButton);
        EditorGUILayout.EndHorizontal();
    }

    private void DrawLaneView()
    {
        EditorGUILayout.BeginHorizontal();
        DrawLaneHeaders();
        DrawLaneBodies();
        EditorGUILayout.EndHorizontal();
    }
    private void DrawLaneHeaders()
    {
        EditorGUILayout.BeginVertical(new GUIStyle(),GUILayout.Width(80));
        EditorGUILayout.LabelField(new GUIContent("ruler"), Styles.ruler, GUILayout.ExpandWidth(true));
        foreach (LaneSO waveDataLane in waveData.lanes)
        {
            EditorGUILayout.LabelField(new GUIContent(waveDataLane.name), Styles.lane, GUILayout.ExpandWidth(true));
        }
        EditorGUILayout.EndVertical();
    }
    private void DrawLaneBodies()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(1200));
        EditorGUILayout.LabelField("ruler", Styles.ruler, GUILayout.ExpandWidth(true));
        foreach (LaneSO waveDataLane in waveData.lanes)
        {
            Rect controlRect = EditorGUILayout.GetControlRect(false, 60, Styles.lane, GUILayout.ExpandWidth(true));
            DrawLaneBody(waveDataLane, controlRect);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }
    private void DrawLaneBody(LaneSO lane, Rect controlRect)
    {
        Handles.color = new Color(.35f,.35f,.35f);
        float mark = 0;
        EditorGUI.DrawRect(controlRect, new Color(.20f,.20f,.20f));
        while (mark < controlRect.width)
        {
            var start = new Vector3(controlRect.x + mark, controlRect.y, 0);
            var end = new Vector3(controlRect.x + mark, controlRect.y + controlRect.height, 0);
            Handles.DrawLine(start, end);
            mark += 10;
        }

        DrawLaneContent(lane, controlRect);
    }
    private void DrawLaneContent(LaneSO lane, Rect controlRect)
    {
        List<Spawn> spawns = waveData.spawns;
        for (var i = 0; i < spawns.Count; i++)
        {
            Spawn spawn = spawns[i];
            EditorGUI.LabelField(new Rect(controlRect) { x = controlRect.x + spawn.StartTime, width = spawn.EndTime - spawn.StartTime}, "box", Styles.spawn);
        }
    }
    private void SpawnsForLane(LaneSO lane)
    {
    }
}