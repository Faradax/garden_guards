using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverEffect : MonoBehaviour
{

    public float fadeDuration = 1;
    public UIDocument uiDocument;
    
    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        var timePassed = 0f;
        var screen = uiDocument.rootVisualElement.Q<VisualElement>("screen");
        screen.style.display = DisplayStyle.Flex;
        while (timePassed < fadeDuration)
        {
            float ratio = timePassed / fadeDuration;
            screen.style.opacity = ratio;
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        screen.style.opacity = 1f;
    }

}
