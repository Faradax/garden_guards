using System.Collections.Generic;
using System.Linq;
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
            border = new RectOffset(15, 15, 10, 10),
            overflow = new RectOffset(0, 0, -5, -5),
            padding = new RectOffset(5, 5, 5, 5),
            alignment = TextAnchor.MiddleCenter
        };
    }

    public class WaveViewSettings
    {
        public bool Snapping { get; set; } = true;
        public float PixelPerSecond { get; set; } = 15;
    } 
    
    public WaveViewSettings settings = new WaveViewSettings();
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
        settings.Snapping = GUILayout.Toggle(settings.Snapping, EditorGUIUtility.TrIconContent("GridAxisX", "Snap to X"),
            EditorStyles.toolbarButton);
        settings.PixelPerSecond = EditorGUILayout.Slider( settings.PixelPerSecond, 0.2f, 30f);
        GUILayout.FlexibleSpace();
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
        EditorGUILayout.Space();
        foreach (LaneSO waveDataLane in waveData.lanes)
        {
            EditorGUILayout.LabelField(new GUIContent(waveDataLane.name), Styles.lane, GUILayout.ExpandWidth(true));
        }
        EditorGUILayout.EndVertical();
    }
    private void DrawLaneBodies()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(300 * settings.PixelPerSecond));
        Rect rulerRect = EditorGUILayout.GetControlRect(false, Styles.ruler.fixedHeight, Styles.ruler, GUILayout.ExpandWidth(true));
        DrawRuler(rulerRect, settings);
        EditorGUILayout.Space();
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
        DrawRuler(controlRect, settings);
        DrawLaneContent(controlRect, lane);
    }
    private static void DrawRuler(Rect controlRect, WaveViewSettings waveViewSettings)
    {
        float pixelsPerSecond = waveViewSettings.PixelPerSecond;
        Handles.color = new Color(.35f, .35f, .35f);
        float mark = 0;
        EditorGUI.DrawRect(controlRect, new Color(.20f, .20f, .20f));
        while (mark < controlRect.width)
        {
            var start = new Vector3(controlRect.x + mark, controlRect.y + 5, 0);
            var end = new Vector3(controlRect.x + mark, controlRect.y + controlRect.height - 5, 0);
            Handles.DrawLine(start, end);
            mark += 5 * pixelsPerSecond;
        }
    }
    private void DrawLaneContent(Rect controlRect, LaneSO lane)
    {
        List<Spawn> spawns = waveData.spawns.Where(it => it.lane = lane).ToList();
        for (var i = 0; i < spawns.Count; i++)
        {
            Spawn spawn = spawns[i];
            SpawnView.DrawSpawn(controlRect, spawn, settings);
        }
    }
}