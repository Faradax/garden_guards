using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionUiController : MonoBehaviour
{
    
    public UIDocument document;
    public VisualTreeAsset card;
    public MousePointer pointer;

    public Draft draft;

    public Compost compost;
    // Start is called before the first frame update
    void Start()
    {
        draft.draftRefresh += OnDraftRefresh;
        OnDraftRefresh();
        VisualElement root = document.rootVisualElement;
        var redrawButton = root.Q<Button>("redraw");
        redrawButton.clickable.clicked += () => draft.OnWaveStart();

    }
    private void OnDraftRefresh()
    {
        IEnumerable<Draft.ShopItem> items = draft.GetItems();

        VisualElement root = document.rootVisualElement;
        var list = root.Q<VisualElement>("buttonList");
        list.Clear();
        
        var i = 0;
        foreach (Draft.ShopItem item in items)
        {
            int index = i;
            TemplateContainer cardInstance = BuildCard(item, index);
            list.Add(cardInstance);
            i++;
        }
    }
    private TemplateContainer BuildCard(Draft.ShopItem item, int index)
    {
        TileSO tileSo = item.TileSO;
        void ChangePlacedItem()
        {
            if (!compost.HasEnough(tileSo.price))
            {
                return;
            }
            compost.Subtract(tileSo.price);
            pointer.placeTile = tileSo;
            draft.ChangeSelection(index);
        }

        void CompostItem()
        {
            compost.Add(tileSo.price);
            draft.Remove(item);
        }
        
        TemplateContainer cardInstance = card.CloneTree();
        cardInstance.Q<Label>("name").text = tileSo.name;
        cardInstance.Q<Button>("use").clicked += ChangePlacedItem;
        cardInstance.Q<Button>("compost").clicked += CompostItem;
        return cardInstance;
    }
}