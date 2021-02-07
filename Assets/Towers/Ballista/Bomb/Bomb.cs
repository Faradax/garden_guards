using Unity.Mathematics;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosion;
    
    private void OnTriggerEnter(Collider other)
    {
        bool isEnemy = other.CompareTag("Enemy");
        if (isEnemy)
        {
            Instantiate(explosion, transform.position, quaternion.identity);
        }
        Destroy(gameObject);
    }
}
