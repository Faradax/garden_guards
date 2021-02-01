using System.ComponentModel;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    public Transform platform;
    public Transform muzzle;

    public GameObject projectile;
        
    private float cooldown = 1.7f;
    private Vector3 _rememberedVelocity;
    private Transform _target;

    public void Update()
    {
        SearchForEnemy();
        cooldown = Mathf.Max(cooldown - Time.deltaTime, 0);

        if (cooldown == 0 && _target)
        {
            Fire();
            cooldown = 1.7f;
        }
    }
    private void Fire()
    {
        GameObject instantiate = Instantiate(projectile, transform.position, Quaternion.identity);
        instantiate.GetComponent<Rigidbody>().velocity = _rememberedVelocity;
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
        if (_target == null)
        {
            return;
        }
        Vector3 toTarget = (_target.position + _target.GetComponent<Rigidbody>().velocity) - transform.position;
        float offsetVertical = toTarget.y;
        Vector3 offsetHorizontal = toTarget;
        offsetHorizontal.y = 0;
        float g = Physics.gravity.y;
        float h = 1.5f;

        var vHorizontal = offsetHorizontal / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (offsetVertical - h) / g));
        var vVertical = Vector3.up * Mathf.Sqrt(-2 * g * h);
        _rememberedVelocity = vHorizontal + vVertical;
        muzzle.transform.LookAt(muzzle.position + (vHorizontal + vVertical), Vector3.up);
        platform.transform.LookAt(platform.position + offsetHorizontal, Vector3.up);
    }
}