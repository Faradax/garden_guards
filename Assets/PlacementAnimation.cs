using System.Collections;
using UnityEngine;

public class PlacementAnimation: MonoBehaviour
{
    public float duration;
    public AnimationCurve blar;

    private void Start()
    {
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        var timePassed = 0f;
        while (timePassed < duration)
        {
            float x = blar.Evaluate(timePassed/duration);
            float y = blar.Evaluate(timePassed/duration);
            float z = blar.Evaluate(timePassed/duration);
            transform.localScale = new Vector3(x, y, z);
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.one;
        Destroy(this);
    }
}