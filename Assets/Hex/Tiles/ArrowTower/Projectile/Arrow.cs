using System.Linq;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float radius = 1;
    public int damage = 1;

    private bool _stuck;

    private void OnTriggerEnter(Collider other)
    {
        if (_stuck) return;
        DoDamage();
        this.transform.position = other.ClosestPoint(transform.position);
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.GetComponent<Collider>());
        transform.parent = other.gameObject.transform;
        _stuck = true;
    }
    private void DoDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (EnemyHealth health in hitColliders.Select(it => it.GetComponent<EnemyHealth>()).Where(it => it))
        {
            health.TakeDamage(damage);
        }
    }
}
