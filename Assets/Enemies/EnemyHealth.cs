using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemySo;

    public int Current { get; set; }
    
    public int Max => enemySo.maxHealth;

    private void Start()
    {
        Current = enemySo.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Current -= amount;

        if (Current <= 0)
        {
            Destroy(gameObject);
        }
    }
}