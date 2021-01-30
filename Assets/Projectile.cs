using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        bool isEnemy = other.CompareTag("Enemy");
        if (isEnemy)
        {
            Debug.Log("Hit");
        }
        Destroy(gameObject);
    }
}
