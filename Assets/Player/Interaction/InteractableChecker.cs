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


        Interactable oldFoundInteractable = _foundInteractable;
        Interactable newFoundInteractable = FindInteractable();

        if (newFoundInteractable == oldFoundInteractable)
        {
            return;
        }

        _foundInteractable = newFoundInteractable;
        
        if (_foundInteractable)
        {
            _foundInteractable.gameObject.AddComponent<Outline>();
        }
        
        if (oldFoundInteractable)
        {
            Destroy(oldFoundInteractable.gameObject.GetComponent<Outline>());
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