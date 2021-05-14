using System;
using UnityEngine;

namespace Enemies
{
[CreateAssetMenu(menuName = "Garden Guards/Counter")]
public class Counter : ScriptableObject
{
    public int initialValue;
    
    public int Value { get; private set; } = 0;

    public event Action<int> ValueChanged;

    private void OnEnable()
    {
        Value = initialValue;
    }

    public void Increase()
    {
        Value++;
        ValueChanged?.Invoke(Value);
    }
    
    public void Decrease()
    {
        Value--;
        ValueChanged?.Invoke(Value);
    }
}
}
