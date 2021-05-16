using UnityEngine;

public class Tile : MonoBehaviour
{

    public TileSO tileSo;
    public bool isPath;
    public Shader darkShader;
    
    private GameObject _thingOnTop;
    private Material _originalMaterial;
    private Material _darkerMaterial;

    private void OnEnable()
    {
        _originalMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        _darkerMaterial = new Material(_originalMaterial);
        _darkerMaterial.shader = darkShader;
    }

    public void OnTowerSelectionChanged(TowerSO towerSo)
    {
        if (!IsEligible(towerSo))
        {
            GetComponent<MeshRenderer>().material = _darkerMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = _originalMaterial;
        }
    }

    public void OnTowerPlaced()
    {
        GetComponent<MeshRenderer>().material = _originalMaterial;
    }

    public bool IsEligible(TowerSO towerSo)
    {
        // TODO: respect Roads, etc
        return !transform.Find("Goal") && towerSo.IsCompatible(this);
    }

    public bool IsPath()
    {
        return isPath;
    }

    public void SpawnTower(TowerSO towerSo)
    {
        GameObject asset = Instantiate(towerSo.asset);
        asset.transform.position = transform.position;
        foreach (Transform child in transform)
        {
            child.parent = null;
            child.gameObject.SetActive(false);
        }
    }
}