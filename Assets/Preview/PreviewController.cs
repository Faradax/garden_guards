using UnityEngine;

public class PreviewController : MonoBehaviour
{
    public Material previewMaterial;

    private TileSO _tileSo;
    private GameObject _preview;

    public void OnTowerSelected(TileSO tileSo)
    {
        if (!tileSo)
        {
            ResetPreview();
        }
        else
        {
            PreparePreview(tileSo);
        }

    }
    private void PreparePreview(TileSO tileSo)
    {
        
        _tileSo = tileSo;

        if (_preview)
        {
            Destroy(_preview);
        }
        _preview = Instantiate(tileSo.asset);
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
        
        ResetPreview();
    }
    private void ResetPreview()
    {

        if (!_preview) return;
        Destroy(_preview);
        _preview = null;
    }

    public void OnTileHovered(Tile tile)
    {
        if (!_preview) return;
        if (tile && tile.tileSo.name == "VoidTile")
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