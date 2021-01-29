using UnityEngine;

public class Ballista : MonoBehaviour
{
    public Transform muzzle;

    public float xAngle;
    
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
        Vector3 toTarget = target.transform.position - transform.position;
        float y = -1;
        toTarget.y = 0;
        float x = toTarget.magnitude;
        
        
        float v = 10;
        float g = Physics.gravity.magnitude;
        xAngle = Mathf.Atan((Mathf.Pow(v, 2) - Mathf.Sqrt(Mathf.Pow(v,4) - g*(g*Mathf.Pow(x,2) + 2*y*Mathf.Pow(v,2))) ) / g * x);

        var quat1 = Quaternion.FromToRotation(Vector3.forward, toTarget.normalized);

        Quaternion quaternion = quat1 * Quaternion.Euler(Mathf.Rad2Deg * xAngle, 0, 0);

        muzzle.transform.rotation = quaternion;
    }
}
