using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowTower : MonoBehaviour
{
    public GameObject projectile;
        
    public float cooldownBetweenSalves = 2.5f;
    public float cooldownBetweenShots = 0.12f;

    private float _salveReadyIn;
    
    private Vector3 _rememberedVelocity;
    private Transform _target;
    public float radius = 2f;
    
    
    public void Update()
    {
        SearchForEnemy();
        _salveReadyIn = Mathf.Max(_salveReadyIn - Time.deltaTime, 0);

        if (_salveReadyIn == 0 && _target)
        {
            Fire();
            _salveReadyIn = cooldownBetweenSalves;
        }
    }
    private void Fire()
    {
        StartCoroutine(SpawnProjectiles());
    }
    private IEnumerator SpawnProjectiles()
    {
        Vector3 transformPosition = _target.transform.position;
        for (var i = 0; i < 20; i++)
        {
            if (_target)
            {
                transformPosition = _target.transform.position;
            }
            SpawnProjectileNearEnemy(transformPosition);
            yield return new WaitForSeconds(cooldownBetweenShots);
        }
    }
    private void SpawnProjectileNearEnemy(Vector3 transformPosition)
    {
        Vector2 offset = Random.insideUnitCircle;
        Vector3 offsetv3 = new(offset.x, 0, offset.y);
        GameObject instantiate = Instantiate(projectile, transformPosition + offsetv3 + Vector3.up * 3f, Quaternion.identity);
        instantiate.GetComponent<Rigidbody>().velocity = Vector3.down * 15f;
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