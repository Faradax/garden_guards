using Enemies;
using UnityEngine;
using UnityEngine.UIElements;

public class HitPointDisplayController : MonoBehaviour
{

    public UIDocument document;
    
    public Counter hitPointCounter;
    private Label _hitPointLabel;
    
    private void OnEnable()
    {
        _hitPointLabel = document.rootVisualElement.Q<Label>("hitPoints");
        hitPointCounter.ValueChanged += UpdateHitPoints;
        UpdateHitPoints(hitPointCounter.Value);
    }

    private void OnDisable()
    {
        hitPointCounter.ValueChanged -= UpdateHitPoints;
    }
    private void UpdateHitPoints(int newAmount)
    {
        _hitPointLabel.text = newAmount.ToString();
    }

}
