using System;
using System.Linq;
using UnityEngine;

namespace Player.Interaction
{
public class InteractableChecker : MonoBehaviour
{
    public int radius = 4;
    public GameObject pointerBillboard;
    
    public Interactable CurrentInteractable
    {
        get => _foundInteractable;
    }
    
    private Interactable _foundInteractable;

    public void Update()
    {
        _foundInteractable = FindInteractable();

        if (_foundInteractable)
        {
            pointerBillboard.transform.position = _foundInteractable.transform.position + Vector3.up;
            pointerBillboard.SetActive(true);
        }
        else
        {
            pointerBillboard.SetActive(false);
        }
    }
    
    private Interactable FindInteractable()
    {
        Collider[] results = Physics.OverlapSphere(transform.position, radius);
        return results.Select(it => it.GetComponent<Interactable>()).FirstOrDefault(it => it);
    }
}
}