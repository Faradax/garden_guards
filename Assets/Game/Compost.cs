using System;
using UnityEngine;

public class Compost : MonoBehaviour
{

    public int max;
    
    [SerializeField]
    [HideInInspector]
    private int current;

    public int Current => current;

    public bool HasEnough(int amount)
    {
        return current >= amount;
    }

    public void Add(int amount)
    {
        current += amount;
    }

    public void Subtract(int amount)
    {
        if (amount > current)
        {
            throw new ArgumentException();
        }
        current -= amount;
    }
}
