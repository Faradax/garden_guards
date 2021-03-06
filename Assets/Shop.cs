using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Shop : MonoBehaviour
{

    public GameEvent onPreparationStart;

    public List<GameObject> shopables;
    private Random _random;

    void Start()
    {
        onPreparationStart.Event.AddListener(Draft);
        Draft();
    }

    private void OnEnable()
    {
        _random = new Random();
    }
    private void Draft()
    {
        for (int i = 0; i < 5; i++)
        {
            var shopable = RandomShopable();
            var position = new Vector3(1.5f, 0.45f, -1.5f + 0.75f * i);
            position += transform.position;
            Instantiate(shopable, position, Quaternion.identity);
        }
    }
    private GameObject RandomShopable()
    {
        int shopablesCount = shopables.Count;
        if (shopablesCount == 0)
        {
            throw new Exception("No shopables available!");
        }
        int randomShopableIndex = _random.Next(shopablesCount);
        return shopables[randomShopableIndex];
    }
}
