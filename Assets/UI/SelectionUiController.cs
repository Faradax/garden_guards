using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionUiController : MonoBehaviour
{
    
    public UIDocument document;
    public MousePointer pointer;

    public Draft draft;
    // Start is called before the first frame update
    void Start()
    {
        draft.draftRefresh += OnDraftRefresh;
        OnDraftRefresh();
    }
    private void OnDraftRefresh()
    {
        IEnumerable<TileSO> towers = draft.GetItems();

        VisualElement root = document.rootVisualElement;
        var list = root.Q<VisualElement>("buttonList");
        list.Clear();
        
        var i = 0;
        foreach (TileSO towerSo in towers)
        {
            int index = i;
            Button button = BuildCard(towerSo, index);
            list.Add(button);
            i++;
        }
    }
    private Button BuildCard(TileSO tileSo, int index)
    {

        void ChangePlacedItem()
        {
            pointer.placeTile = tileSo;
            draft.ChangeSelection(index);
        }

        var button = new Button(ChangePlacedItem) {text = tileSo.name};
        return button;
    }
}