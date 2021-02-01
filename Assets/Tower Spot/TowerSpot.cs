using Player.Interaction;
using UnityEngine;

namespace Tower_Spot
{
public class TowerSpot : Interactable
{

    public override void OnTryInteract(InteractionController interactionController)
    {
        interactionController.Interact(this);
    }

    public void AcceptSeed(Transform seed)
    {
        seed.position = transform.position;
        seed.GetComponent<TowerCapsule>().Use();
        gameObject.SetActive(false);
    }
}
}