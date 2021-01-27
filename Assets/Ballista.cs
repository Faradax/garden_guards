using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    public Transform muzzle;

    public void Update()
    {
        SearchForEnemy();
    }
    
    private void SearchForEnemy()
    {
        Collider[] overlapSphere = Physics.OverlapSphere(transform.position, 5f);

        Transform target = null;
        foreach (Collider collider1 in overlapSphere)
        {
            bool isEnemy = collider1.CompareTag("Enemy");
            if (isEnemy)
            {
                target = collider1.transform;
                break;
            }
        }
        if (target == null)
        {
            return;
        }
        muzzle.LookAt(target.position);
    }
}
