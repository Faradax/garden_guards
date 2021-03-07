using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Garden Guards/Tower")]
public class TowerSO : ScriptableObject
{
    public GameObject asset;
    public GameObject previewAsset;
    [Min(0)]
    public int price;
}
