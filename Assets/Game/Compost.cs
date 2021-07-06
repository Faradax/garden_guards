using System;
using UnityEngine;
using UnityEngine.Events;

public class Compost : MonoBehaviour
{

    public int max;
    
    [SerializeField]
    [HideInInspector]
    private int current;

    public int Current => current;
    public UnityEvent<int> valueChanged;

    public bool HasEnough(int amount)
    {
        return current >= amount;
    }

    public void Add(int amount)
    {
        current += amount;
        valueChanged.Invoke(current);
    }

    public void Subtract(int amount)
    {
        if (amount > current)
        {
            throw new ArgumentException();
        }
        current -= amount;
        valueChanged.Invoke(current);
    }
}
