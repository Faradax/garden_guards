using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class MousePointer : MonoBehaviour
{
    public TowerSO placeTower;
    public Texture2D pointerTexture; 
    
    private Camera _camera;

    private Clickable _hovered;
    private void Start()
    {
        _camera = GetComponent<Camera>();
        
        Cursor.SetCursor(pointerTexture, Vector2.zero, CursorMode.Auto);
    }

    [UsedImplicitly]
    public void OnPoint(InputAction.CallbackContext context)
    {
        var pointerPosition = context.ReadValue<Vector2>();
        Clickable currentlyHovered = CurrentlyHovered(pointerPosition);
        if (currentlyHovered == _hovered) return;
        if (_hovered)
        {
            _hovered.OnMouseExit();
            _hovered = null;
        }
        if (currentlyHovered)
        {
            currentlyHovered.OnMouseEnter();
            _hovered = currentlyHovered;
        }
    }

    [UsedImplicitly]
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton()) return;
        if (_hovered)
        {
            _hovered.OnClick();
            GameObject instantiate = Instantiate(placeTower.asset);
            instantiate.transform.position = _hovered.transform.position;
        }
    }
    
    private Clickable CurrentlyHovered(Vector2 pointerPosition)
    {

        Ray pointerRay = _camera.ScreenPointToRay(pointerPosition);
        bool madeContact = Physics.Raycast(pointerRay, out RaycastHit hit);
        
        if (!madeContact) return null;
        
        GameObject colliderGameObject = hit.collider.gameObject;
        
        if (!colliderGameObject) return null;
        
        var clickable = colliderGameObject.GetComponent<Clickable>();
        
        return clickable ? clickable : null;

    }
}
