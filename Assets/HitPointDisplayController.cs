using Enemies;
using UnityEngine;
using UnityEngine.UIElements;

public class HitPointDisplayController : MonoBehaviour
{

    public UIDocument document;
    
    public Counter hitPointCounter;
    public Countdown preparationCountdown;
    private Label _hitPointLabel;
    
    private VisualElement _preparationCountdown;
    private Label _remainingTimeLabel;

    private void OnEnable()
    {
        _hitPointLabel = document.rootVisualElement.Q<Label>("hitPoints");
        _remainingTimeLabel = document.rootVisualElement.Q<Label>("remainingTime");
        _preparationCountdown = document.rootVisualElement.Q<VisualElement>("preparationCountdown");
        
        hitPointCounter.ValueChanged += UpdateHitPoints;
        preparationCountdown.valueChanged.AddListener(UpdateCountdown);
        
        UpdateHitPoints(hitPointCounter.Value);
    }
    private void UpdateCountdown(int newValue)
    {
        _remainingTimeLabel.text = newValue.ToString();
        _preparationCountdown.style.display = newValue == 0 ? DisplayStyle.None : DisplayStyle.Flex;
    }

    private void OnDisable()
    {
        hitPointCounter.ValueChanged -= UpdateHitPoints;
        preparationCountdown.valueChanged.RemoveListener(UpdateCountdown);
    }
    private void UpdateHitPoints(int newAmount)
    {
        _hitPointLabel.text = newAmount.ToString();
    }

}
