using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{

    public TileSO tileSo;
    public bool isPath;
    public bool isIrreplaceable;
    public Shader darkShader;

    public UnityEvent tilePlaced;
    public UnityEvent tileRemoved;

    private GameObject _thingOnTop;
    private Material _originalMaterial;
    private Material _darkerMaterial;

    private void OnEnable()
    {
        _originalMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        _darkerMaterial = new Material(_originalMaterial);
        _darkerMaterial.shader = darkShader;
    }
    
    public bool IsEligible(TileSO newTileSo)
    {
        // TODO: respect Roads, etc
        return !transform.Find("Goal") && !isIrreplaceable && tileSo.canUpgradeTo(newTileSo);
    }

    public bool IsPath()
    {
        return isPath;
    }

    private void Drop()
    {
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.AddForceAtPosition(Vector3.up * 2, Vector3.forward);
        rigidbody.drag = .7f;
    }

    public bool isEmpty()
    {
        return tileSo.asset.name == "VoidTile";
    }
}