using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Player
{
[CreateAssetMenu(menuName = "Garden Guards/Wealth")]
public class Wealth : ScriptableObject
{
    public UnityEvent NotEnough = new UnityEvent();
    public UnityEvent Changed = new UnityEvent();

    public int Amount { get; private set; } = 500;

    private void OnEnable()
    {
        Amount = 500;
    }

    public void Add(int addition)
    {
        Amount += addition;
        Changed.Invoke();
    }

    public bool Reduce(int reduction)
    {
        if (Amount >= reduction)
        {
            Amount -= reduction;
            Changed.Invoke();
            return true;
        }
        NotEnough.Invoke();
        return false;
    }
}
}