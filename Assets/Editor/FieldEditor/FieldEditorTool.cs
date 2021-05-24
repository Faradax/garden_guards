using System.Collections.Generic;
using System.Linq;
using Editor.FieldEditor;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Overlays;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;


// The second argument in the EditorToolAttribute flags this as a Component tool. That means that it will be instantiated
// and destroyed along with the selection. EditorTool.targets will contain the selected objects matching the type.
[EditorTool("Field Tool", componentToolTarget: typeof(HexMap))]
class FieldTool : EditorTool, IDrawSelectedHandles
{
    // Global tools (tools that do not specify a target type in the attribute) are lazy initialized and persisted by
    // a ToolManage. Component tools (like this example) are instantiated and destroyed with the current selection.
    void OnEnable()
    {
        // Allocate unmanaged resources or perform one-time set up functions here
    }

    void OnDisable()
    {
        // Free unmanaged resources, state teardown.
    }

    // The second "context" argument accepts an EditorWindow type.
    [Shortcut("Activate Field Tool", typeof(SceneView), KeyCode.P, ShortcutModifiers.Alt)]
    static void FieldToolShortcut()
    {
        if (Selection.GetFiltered<HexMap>(SelectionMode.TopLevel).Length > 0)
            ToolManager.SetActiveTool<FieldTool>();
    }

    // Called when the active tool is set to this tool instance. Global tools are persisted by the ToolManager,
    // so usually you would use OnEnable and OnDisable to manage native resources, and OnActivated/OnWillBeDeactivated
    // to set up state. See also `EditorTools.{ activeToolChanged, activeToolChanged }` events.
    public override void OnActivated()
    {
        SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Entering Field Tool"), .1f);
    }

    // Called before the active tool is changed, or destroyed. The exception to this rule is if you have manually
    // destroyed this tool (ex, calling `Destroy(this)` will skip the OnWillBeDeactivated invocation).
    public override void OnWillBeDeactivated()
    {
        SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Exiting Field Tool"), .1f);
    }

    // Equivalent to Editor.OnSceneGUI.
    public override void OnToolGUI(EditorWindow window)
    {
        Event current = Event.current;
        if (current.type == EventType.MouseDown && current.button == 0)
        {
            TileSO selection = FieldEditorState.instance.Selection;
            if (!selection) return;

            Ray ray = HandleUtility.GUIPointToWorldRay(current.mousePosition);
            var plane = new Plane(Vector3.up, Vector3.zero);
            bool raycast = plane.Raycast(ray, out float distance);
            if (raycast)
            {
                Vector3 worldClick = ray.GetPoint(distance);
                Debug.Log("Now spawning" + selection);


                Vector2 axialCoords = HexMap.WorldToAxial(worldClick);
                Vector3 cube = HexMap.AxialToCube(axialCoords);
                Vector3 cubeRound = HexMap.CubeRound(cube);
                Vector2 axialRound = HexMap.CubeToAxial(cubeRound);
                Vector2 units = HexMap.AxialToWorld(axialRound);

                Instantiate(selection.asset, new Vector3(units.x, 0, units.y), Quaternion.identity);
            }
        }
        // Don't allow clicking over empty space to deselect the object
        if (current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(0);
        }
    }

    public void OnDrawHandles()
    {
    }
}

[Overlay(typeof(SceneView), k_Id, "My Awesome Overlay")]
class MyOverlay : Overlay, ICreateHorizontalToolbar, ICreateVerticalToolbar
{
    const string k_Id = "my-custom-overlay";

    public override VisualElement CreatePanelContent()
    {
        var refs = AssetDatabase.FindAssets("t:TileSO");
        List<TileSO> tileSos = refs.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<TileSO>).ToList();
        var myContent = new VisualElement {name = "my-content"};
        foreach (TileSO tileSo in tileSos)
        {
            myContent.Add(new Button(() => FieldEditorState.instance.ChangeSelection(tileSo)) {text = tileSo.name});
        }
        return myContent;
    }

    public VisualElement CreateHorizontalToolbarContent()
    {
        var myContent = new VisualElement {name = "my-horizontal-content"};
        return myContent;

    }

    public VisualElement CreateVerticalToolbarContent()
    {
        var myContent = new VisualElement {name = "my-vertical-content"};
        return myContent;
    }
}