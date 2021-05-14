using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Collider))]
public class Clickable : MonoBehaviour
{
    private Outline _outline;

    private void OnEnable()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void OnMouseEnter()
    {
        _outline.enabled = true;
    }
    
    public void OnMouseExit()
    {
        _outline.enabled = false;
    }
    public void OnClick()
    {
    }
}
