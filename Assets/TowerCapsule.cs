using Player.Interaction;
using UnityEngine;

public class TowerCapsule : Interactable
{
    public GameObject towerAsset;
    
    public override void OnTryInteract(InteractionController interactionController)
    {
        interactionController.PickUp(gameObject);
    }
    
    public void Use()
    {
        Instantiate(towerAsset, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
