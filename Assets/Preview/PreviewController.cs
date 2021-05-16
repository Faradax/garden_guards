using UnityEngine;

public class PreviewController : MonoBehaviour
{
    public Material previewMaterial;
    
    private TowerSO _towerSo;
    private GameObject _preview;
    
    public void OnTowerSelected(TowerSO towerSo)
    {
        _towerSo = towerSo;
        
        if (_preview)
        {
            Destroy(_preview);
        }
        _preview = Instantiate(towerSo.asset);
        _preview.SetActive(false);
        ChangeMaterial();
        StripBehaviours();

    }
    private void ChangeMaterial()
    {

        MeshRenderer[] meshRenderers = _preview.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = previewMaterial;
        }
    }
    private void StripBehaviours()
    {

        MonoBehaviour[] behaviours = _preview.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour monoBehaviour in behaviours)
        {
            monoBehaviour.enabled = false;
        }
    }

    public void OnTowerPlaced()
    {
        if (_preview)
        {
            Destroy(_preview);
            _preview = null;
        }
    }
    
    public void OnTileHovered(Tile tile)
    {
        if (!_preview) return;
        if (tile && tile.IsEligible(_towerSo))
        {
            _preview.SetActive(true);
            _preview.transform.position = tile.transform.position;
        }
        else
        {
            _preview.SetActive(false);
        }
    }

}
