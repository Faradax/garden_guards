using UnityEngine;
using UnityEngine.UIElements;

public class InfoUiController : MonoBehaviour
{
    public UIDocument document;
    private Label _tileName;

    private void Start()
    {
        _tileName = document.rootVisualElement.Q<Label>("tileName");
    }

    public void DisplayTileInfo(Tile tile)
    {
        _tileName.text = tile ? tile.name : "";
    }
}