using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemySo;

    public int health;

    private void Start()
    {
        health = enemySo.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}