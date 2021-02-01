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
    private Func<Interactable, bool> _filter;

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
        return results
            .Select(it => it.GetComponent<Interactable>())
            .Where(it => it)
            .Where(_filter)
            .OrderBy(it => Vector3.SqrMagnitude(it.transform.position - transform.position))
            .FirstOrDefault();
    }
    public void SetFilterMethod(Func<Interactable, bool> filter)
    {
        _filter = filter;
    }
}
}