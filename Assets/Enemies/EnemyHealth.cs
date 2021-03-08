using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemySo;
    private Material _flashMat;
    private Renderer _renderer;
    private Material _orignalMaterial;

    public int Current { get; set; }
    
    public int Max => enemySo.maxHealth;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _flashMat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        _flashMat.color = Color.white;
        _orignalMaterial = _renderer.material;
        Current = enemySo.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Current -= amount;

        StartCoroutine(Flash());
        
        if (Current <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        for (var i = 0; i < enemySo.lootAmount; i++)
        {
            Instantiate(enemySo.lootAsset, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
    private IEnumerator Flash()
    {
        _renderer.material = _flashMat;
        yield return new WaitForSeconds(0.025f);
        _renderer.material = _orignalMaterial;

    }

    private void OnEnable()
    {
        enemySo.counter.Increase();
    }
    
    private void OnDisable()
    {
        enemySo.counter.Decrease();
    }
}