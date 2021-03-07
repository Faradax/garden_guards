using System;
using DefaultNamespace.Shop;
using Player.Interaction;
using UnityEngine;

public class ItemStand : Interactable
{

    public ShopItemSO item;

    private GameObject preview;

    private bool hasItem = false;
    
    private void OnEnable()
    {
        preview = Instantiate(item.previewAsset, transform.position + Vector3.up, Quaternion.identity);
        hasItem = true;
    }

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
        hasItem = false;
    }

    public override bool IsInteractable()
    {
        return hasItem;
    }
}