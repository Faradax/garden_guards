using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosion;
    
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        DoDamage();
        Destroy(gameObject);
    }
    private void DoDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1);
        foreach (EnemyHealth health in hitColliders.Select(it => it.GetComponent<EnemyHealth>()).Where(it => it))
        {
            health.TakeDamage(1);
        }
    }
}
