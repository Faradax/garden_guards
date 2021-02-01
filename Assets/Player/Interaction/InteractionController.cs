using Tower_Spot;
using UnityEngine;

namespace Player.Interaction
{
public class InteractionController : MonoBehaviour
{
    private Transform _carryableTransform;

    public InteractableChecker interactableChecker;

    /**
     * General-Purpose entrypoint for interaction with whatever is there
     */
    public void Interact()
    {
        Interactable interactable = interactableChecker.CurrentInteractable;
        if (interactable)
        {
            interactable.OnTryInteract(this);
        }
    }

    /**
     * Carrying can be cancelled (drop item).
     */
    public void Cancel()
    {
        if (_carryableTransform == null) return;
        Drop();
    }

    /**
     * Specific Method that Interactables can call to place an interactable in the players hands.
     */
    // TODO: Maybe Pickup-Interface later instead of GameObject
    public void PickUp(GameObject itemToPickUp)
    {
        // Guard: Only one item can be carried at the same time.
        if (_carryableTransform) return;

        _carryableTransform = itemToPickUp.transform;
        _carryableTransform.parent = transform;
        _carryableTransform.localPosition = transform.forward;
    }

    private void Drop()
    {

        _carryableTransform.parent = null;
        _carryableTransform = null;
    }

    /**
     * Specific method that Tower Spots can call to have the player plant their seed.
     */
    public void Interact(TowerSpot towerSpot)
    {
        if (_carryableTransform)
        {
            towerSpot.AcceptSeed(_carryableTransform);
        }
    }
}
}