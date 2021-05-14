using Enemies;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Counter hitPointCounter;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            hitPointCounter.Decrease();
        }
    }
}
