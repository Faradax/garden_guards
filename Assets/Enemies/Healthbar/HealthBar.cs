using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public EnemyHealth enemyHealth;

    public Slider slider;
    
    private void OnEnable()
    {
        slider.maxValue = enemyHealth.Max;
    }

    private void Update()
    {
        slider.value = enemyHealth.Current;
    }
}
