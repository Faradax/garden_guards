using Player.Interaction;
using UnityEngine;

public class ItemStand : Interactable
{

    public GameObject shopable;
    
    public override void OnTryInteract(InteractionController interactionController)
    {
        interactionController.Buy(200);
        GameObject boughtItem = Instantiate(shopable, transform.position, Quaternion.identity);
        interactionController.PickUp(boughtItem);
    }
}