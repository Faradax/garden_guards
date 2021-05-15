using Enemies;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Counter hitPointCounter;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hitPointCounter.Value == 0) return;
        if (!other.CompareTag("Enemy")) return;
        
        Destroy(other.gameObject);
        hitPointCounter.Decrease();
    }
}
