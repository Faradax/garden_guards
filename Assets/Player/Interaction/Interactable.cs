using UnityEngine;

namespace Player.Interaction
{
public abstract class Interactable : MonoBehaviour
{
    public abstract void OnTryInteract(InteractionController interactionController);
}
}