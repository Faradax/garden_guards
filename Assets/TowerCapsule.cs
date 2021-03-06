using Player.Interaction;
using UnityEngine;

public class TowerCapsule : Interactable
{
    public GameObject towerAsset;
    public bool bought;
    
    public override void OnTryInteract(InteractionController interactionController)
    {
        if (!bought)
        {
            interactionController.Buy(200);
            bought = true;
        }
        interactionController.PickUp(gameObject);
    }
    
    public void Use()
    {
        Instantiate(towerAsset, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
