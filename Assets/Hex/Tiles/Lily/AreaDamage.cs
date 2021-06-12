using System.Collections.Generic;
using UnityEngine;

namespace Hex.Tiles.Lily
{
public class AreaDamage : MonoBehaviour
{
    public int damage;

    private HashSet<EnemyHealth> _knownEnemies = new();

    public void DoDamage()
    {
        foreach (EnemyHealth enemyHealth in _knownEnemies)
        {
            if (enemyHealth == null)
            {
                continue;
            }
            enemyHealth.TakeDamage(damage);
        }
    }
    
    private void OnEnable()
    {
        _knownEnemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (!enemyHealth) return;
        _knownEnemies.Add(enemyHealth);
    }

    private void OnTriggerExit(Collider other)
    {
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (!enemyHealth) return;
        _knownEnemies.Remove(enemyHealth);
    }
}
}