using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInVectorField : MonoBehaviour
{

    public VectorMapper map;
    private Vector3 _desiredDirection;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        _desiredDirection = map.Sample(currentPosition);;
        
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _desiredDirection * 5;
    }
}
