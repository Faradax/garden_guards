using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : MonoBehaviour
{
    public GameObject projectileEffect;
    private float cooldown = 0.2f;
    private Transform _target;

    public void Update()
    {
        SearchForEnemy();
        cooldown = Mathf.Max(cooldown - Time.deltaTime, 0);

        if (cooldown == 0 && _target)
        {
            Fire();
            cooldown = 0.2f;
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
        Collider[] overlapSphere = Physics.OverlapSphere(transform.position, 5f);

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
