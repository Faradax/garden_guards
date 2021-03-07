using System;
using DefaultNamespace.Shop;
using Player.Interaction;
using UnityEngine;

public class ItemStand : Interactable
{

    private ShopItemSO item;

    private GameObject preview;

    public bool HasItem { get; private set; }

    private void OnDisable()
    {
        Destroy(preview);
    }

    public override void OnTryInteract(InteractionController interactionController)
    {
        if (item == null) return;
        
        interactionController.Buy(item.price);
        GameObject boughtItem = Instantiate(item.previewAsset, transform.position, Quaternion.identity);
        interactionController.PickUp(boughtItem);
        Destroy(preview);
        item = null;
        HasItem = false;
    }

    public override bool IsInteractable()
    {
        return HasItem;
    }

    public void UpdateItem(ShopItemSO randomShopItem)
    {
        item = randomShopItem;
        if (preview) { Destroy(preview); }
        preview = Instantiate(item.previewAsset, transform.position + Vector3.up, Quaternion.identity);
        preview.transform.parent = transform;
        HasItem = true;
    }
}