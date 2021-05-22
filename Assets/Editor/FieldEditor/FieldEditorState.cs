using System;
using UnityEditor;
using UnityEngine;

namespace Editor.FieldEditor
{
public class FieldEditorState : ScriptableSingleton<FieldEditorState>
{
    public event Action<TileSO> SelectionChanged;

    [SerializeField]
    public TileSO Selection { get; private set; }

    public void ChangeSelection(TileSO tileSo)
    {
        Selection = tileSo;
        SelectionChanged?.Invoke(tileSo);
    }
}
}