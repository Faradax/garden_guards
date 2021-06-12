using Hex.Tiles.Lily;
using UnityEngine;

public class LilyTile : MonoBehaviour
{

    public AreaDamage areaDamage;
    public float cooldown;
    public float currentCooldown;

    private void Update()
    {
        currentCooldown = Mathf.Max(currentCooldown - Time.deltaTime, 0);

        if (currentCooldown == 0)
        {
            Fire();
            currentCooldown = cooldown;
        }
    }
    
    private void Fire()
    {
        areaDamage.DoDamage();
    }
}