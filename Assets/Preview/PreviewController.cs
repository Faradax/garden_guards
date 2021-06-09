using UnityEngine;
using UnityEngine.InputSystem;

public class PreviewController : MonoBehaviour
{
    public Material previewMaterial;

    private TileSO _tileSo;
    private GameObject _preview;
    private int _angle;
    public float maxDistanceDelta = 0.1f;

    private bool _hoveringTile;
    private Vector3 _mousePosition;
    private Vector3 _hoveredTilePosition;
    private Quaternion _desiredRotation;

    public void Show(TileSO tileSo)
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
        _preview = Instantiate(tileSo.asset, _mousePosition, _desiredRotation);
        StripBehaviours();
    }

    public void End()
    {

        ResetPreview();
    }

    private void Update()
    {
        if (_preview && _preview.activeInHierarchy)
        {
            AdjustPosition();
            AdjustRotation();
        }
    }

    public void OnTileHovered(Tile tile)
    {
        if (!_preview) return;
        if (tile && tile.tileSo.name == "VoidTile")
        {
            _hoveringTile = true;
            _hoveredTilePosition = tile.transform.position;
        }
        else
        {
            _hoveringTile = false;
        }
    }

    public void OnMouseMove(InputAction.CallbackContext ctx)
    {
        var pointerPosition = ctx.ReadValue<Vector2>();
        Ray pointerRay = Camera.main.ScreenPointToRay(pointerPosition);
        var plane = new Plane(Vector3.up, 2 * Vector3.up);
        bool madeContact = plane.Raycast(pointerRay, out float distance);
        if (madeContact)
        {
            _mousePosition = pointerRay.GetPoint(distance);
        }
    }

    public void UpdateAngle(int angle)
    {
        _desiredRotation = Quaternion.Euler(0, angle, 0);
    }

    private void AdjustPosition()
    {

        var targetPos = _hoveringTile ? _hoveredTilePosition : _mousePosition;
        targetPos += Vector3.up * (Mathf.Sin(Time.time * 2) * 0.1f + 0.11f);
        maxDistanceDelta = 0.4f;
        Vector3 newPos = Vector3.MoveTowards(_preview.transform.position, targetPos, maxDistanceDelta);
        _preview.transform.position = newPos;
    }
    private void AdjustRotation()
    {

        _preview.transform.rotation = Quaternion.RotateTowards(_preview.transform.rotation, _desiredRotation, 3f);
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
        _preview.GetComponent<Collider>().enabled = false;
    }
    private void ResetPreview()
    {

        if (!_preview) return;
        Destroy(_preview);
        _preview = null;
        _hoveringTile = false;
    }
}