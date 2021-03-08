using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Player;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{

    public Wealth wealth; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            wealth.Add(5);
            Destroy(other.gameObject);
        }
    }

}
