using Unity.Mathematics;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosion;
    
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
