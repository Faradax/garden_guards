using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Player;
using Enemies;
using Player.Interaction;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Counter allEnemiesCounter;
    public Wealth wealth;
    private int representedWealthAmount;
    public Text text;

    public AnimationCurve curve;

    void Start()
    {
        wealth.Changed.AddListener(OnValueChange);
        wealth.NotEnough.AddListener(() => Debug.Log("Not enough Moneys"));
    }

    private void Update()
    {
        int difference = wealth.Amount - representedWealthAmount;
        if (difference == 0) return;
        int sign = Math.Sign(difference);
        representedWealthAmount += sign;
        text.text = representedWealthAmount.ToString();
    }

    private void OnValueChange()
    {
        StartCoroutine(Wobble());
    }

    private IEnumerator Wobble()
    {
        float elapsedTime = 0;
        while (elapsedTime < 5)
        {
            elapsedTime += Time.deltaTime;
            float size = curve.Evaluate(Time.time % 1);
            text.transform.localScale = new Vector3(size, size, 1);
            yield return new WaitForEndOfFrame();
        }
        text.transform.localScale = Vector3.one;
    }
}