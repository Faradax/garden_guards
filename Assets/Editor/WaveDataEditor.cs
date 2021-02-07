using DefaultNamespace;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Editor
{

public static class AssetHandler
{
    [OnOpenAsset]
    public static bool HandleAsset(int instanceId, int line)
    {
        WaveData waveData = EditorUtility.InstanceIDToObject(instanceId) as WaveData;
        if (waveData != null)
        {
            WaveDataEditorWindow.Open(waveData);
        }
        return false;
    }
}

[CustomEditor(typeof(WaveData))]
public class WaveDataEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
}