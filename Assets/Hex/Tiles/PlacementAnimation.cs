using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAnimation: TileBehaviour
{
    public float duration;
    public AnimationCurve blar;

    public override void OnTilePlaced(List<Tile> neighbours)
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