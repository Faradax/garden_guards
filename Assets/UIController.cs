using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Counter allEnemiesCounter;
    public Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        allEnemiesCounter.ValueChanged += it => text.text = it.ToString();
    }
}
