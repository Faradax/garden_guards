using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : MonoBehaviour
{
    public GameObject projectileEffect;
    public float cooldown = 0.2f;
    private float _currentCooldown;
    private Transform _target;
    public float radius = 2;

    public void Update()
    {
        SearchForEnemy();
        _currentCooldown = Mathf.Max(_currentCooldown - Time.deltaTime, 0);

        if (_currentCooldown == 0 && _target)
        {
            Fire();
            _currentCooldown = cooldown;
        }
    }
    private void Fire()
    {
        _target.GetComponent<EnemyHealth>()?.TakeDamage(1);
        GameObject effect = Instantiate(projectileEffect, _target.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        Destroy(effect, 0.1f);
    }

    private void SearchForEnemy()
    {
        Collider[] overlapSphere = Physics.OverlapSphere(transform.position, radius);

        _target = null;
        foreach (Collider collider1 in overlapSphere)
        {
            bool isEnemy = collider1.CompareTag("Enemy");
            if (isEnemy)
            {
                _target = collider1.transform;
                break;
            }
        }
    }
}
