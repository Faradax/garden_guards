using DefaultNamespace;
using UnityEditor;
using UnityEngine;

static internal class SpawnView
{
    public static void DrawSpawn(Rect controlRect, Spawn spawn, WaveDataEditorWindow.WaveViewSettings waveViewSettings)
    {
        float startPixel = controlRect.x + (spawn.StartTime * waveViewSettings.PixelPerSecond);
        float widthPixel = (spawn.EndTime - spawn.StartTime) * waveViewSettings.PixelPerSecond;
        var spawnRect = new Rect(controlRect) {x = startPixel, width = widthPixel};
        EditorGUI.LabelField(spawnRect,
            "box",
            WaveDataEditorWindow.Styles.spawn);
        EditorGUIUtility.AddCursorRect(spawnRect, MouseCursor.Pan);
    }
}