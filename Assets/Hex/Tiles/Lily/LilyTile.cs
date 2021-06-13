using System.Collections;
using Hex.Tiles.Lily;
using UnityEngine;

public class LilyTile : MonoBehaviour
{

    public AreaDamage areaDamage;
    public GameObject beamFx;
    public float cooldown = 2;
    public float currentCooldown;
    public float damageInterval = 0.3f;

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
        StartCoroutine(DoFire());
    }
    private IEnumerator DoFire()
    {
        beamFx.SetActive(true);
        for (var i = 0; i < 2; i++)
        {
            areaDamage.DoDamage();
            yield return new WaitForSeconds(damageInterval);
        }
        beamFx.SetActive(false);
    }
}