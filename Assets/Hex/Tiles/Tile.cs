using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{

    public TileSO tileSo;
    public bool isPath;
    public bool isIrreplaceable;
    public Shader darkShader;
    public GameObject border;
    
    public UnityEvent tilePlaced;
    public UnityEvent tileRemoved;

    private Material _originalMaterial;
    private Material _darkerMaterial;
    private UpgradeCondition _upgradeCondition;
    private HexMap _hexMap;

    private void OnEnable()
    {
        _hexMap = HexMap.instance;
        _upgradeCondition = GetComponent<UpgradeCondition>();

        _originalMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        
        _darkerMaterial = new Material(_originalMaterial);
        _darkerMaterial.shader = darkShader;
    }

    public void OnNeighboursChanged(List<Tile> neighbours)
    {
        if (!_upgradeCondition) return;
        _upgradeCondition.EvaluateUpgradeCondition(neighbours);
        //border.SetActive(_upgradeCondition.IsFulfilled());
    }
    public void OnClick()
    {
        _upgradeCondition?.DoUpgrade();
    }

    public void Drop()
    {
        StartCoroutine(DoDrop());
    }
    private IEnumerator DoDrop()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForceAtPosition(Vector3.down, rb.position + Vector3.right / 2);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}