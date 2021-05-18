using UnityEngine;

public class Tile : MonoBehaviour
{

    public TileSO tileSo;
    public bool isPath;
    public bool isIrreplaceable;
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

    public void OnTowerSelectionChanged(TileSO tileSo)
    {
        if (tileSo && !IsEligible(tileSo))
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

    public bool IsEligible(TileSO tileSo)
    {
        // TODO: respect Roads, etc
        return !transform.Find("Goal") && !isIrreplaceable && this.tileSo.canUpgradeTo(tileSo);
    }

    public bool IsPath()
    {
        return isPath;
    }

    public void SpawnTower(TileSO tileSo)
    {
        Instantiate(tileSo.asset, transform.position, Quaternion.identity);
        
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}