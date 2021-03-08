using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public int rotPerSecond = 60;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotPerSecond * Time.deltaTime);
    }
}