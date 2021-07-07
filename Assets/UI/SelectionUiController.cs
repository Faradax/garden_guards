using System;
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

    private void OnEnable()
    {
        compost.valueChanged.AddListener(value => document.rootVisualElement.Q<Label>("compost").text = value.ToString());
    }

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
            TemplateContainer cardInstance = BuildCard(item);
            list.Add(cardInstance);
            i++;
        }
    }
    private TemplateContainer BuildCard(Draft.ShopItem item)
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
            draft.ChangeSelection(item);
            
        }

        void CompostItem()
        {
            compost.Add(1);
            draft.Remove(item);
        }
        
        TemplateContainer cardInstance = card.CloneTree();
        cardInstance.Q<Label>("name").text = tileSo.name;
        cardInstance.Q<Label>("cost").text = tileSo.price.ToString();
        cardInstance.Q<Button>("use").clicked += ChangePlacedItem;
        cardInstance.Q<Button>("compost").clicked += CompostItem;
        return cardInstance;
    }
}