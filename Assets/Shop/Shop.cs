using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Shop;
using UnityEngine;
using Random = System.Random;

public class Shop : MonoBehaviour
{

    public GameEvent onPreparationStart;

    public List<ShopItemSO> shopItems;
    private List<ItemStand> itemStands;
    private Random _random;

    void Start()
    {
        onPreparationStart.Event.AddListener(Draft);
        Draft();
    }

    private void OnEnable()
    {
        _random = new Random();
        itemStands = GetComponentsInChildren<ItemStand>().ToList();
    }
    
    private void Draft()
    {
        foreach (ItemStand itemStand in itemStands.Where(itemStand => !itemStand.HasItem))
        {
            itemStand.UpdateItem(RandomShopItem());
        }
    }
    public ShopItemSO RandomShopItem()
    {
        int shopablesCount = shopItems.Count;
        if (shopablesCount == 0)
        {
            throw new Exception("No shopables available!");
        }
        int randomShopableIndex = _random.Next(shopablesCount);
        return shopItems[randomShopableIndex];
    }
}
