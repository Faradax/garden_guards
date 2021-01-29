using System.ComponentModel;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    public Transform platform;
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
        Vector3 toTarget = target.position - transform.position;
        float offsetVertical = toTarget.y;
        Vector3 offsetHorizontal = toTarget;
        offsetHorizontal.y = 0;
        float g = Physics.gravity.y;
        float h = 1.5f;
        Vector3 u_right = offsetHorizontal / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (offsetVertical - h) / g));
        float u_up = Mathf.Sqrt(-2 * g * h);

        var vHorizontal = u_right;
        var vVertical = Vector3.up * u_up;
        Debug.Log(vHorizontal + vVertical);
        muzzle.transform.LookAt(muzzle.position + (vHorizontal + vVertical), Vector3.up);
    }
}