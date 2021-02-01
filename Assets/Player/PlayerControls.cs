using System.Collections;
using JetBrains.Annotations;
using Player.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour
{
    public float maxAcceleration = 5;
    public float maxSpeed = 4;
    
    private Rigidbody _rigidbody;
    private Vector3 _desiredVelocity;
    private Vector3 _groundNormal;

    public void Start()
    {
        Debug.Log("Start");

        _rigidbody = GetComponent<Rigidbody>();
    }

        
    [UsedImplicitly]
    public void AcceptDirection(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        var input = context.ReadValue<Vector2>();
        _desiredVelocity = new Vector3(input.x, 0, input.y) * maxSpeed;
    }
    
    [UsedImplicitly]
    public void AcceptInteract(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        GetComponent<InteractionController>()?.Interact();
    }
    
    [UsedImplicitly]
    public void AcceptCancel(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        GetComponent<InteractionController>()?.Cancel();
    }
    
    void FixedUpdate()
    {

        var velocity = _rigidbody.velocity;
        var maxDelta = maxAcceleration * Time.deltaTime;

        // adapt desired velocity to ground inclination
        var xAxis = Vector3.ProjectOnPlane(Vector3.right, _groundNormal);
        var zAxis = Vector3.ProjectOnPlane(Vector3.forward, _groundNormal);
        float currentX = Vector3.Dot(velocity, xAxis);
        float currentZ = Vector3.Dot(velocity, zAxis);

        var newX = Mathf.MoveTowards(currentX, _desiredVelocity.x, maxDelta);
        var newZ = Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxDelta);

        _rigidbody.velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    private void OnCollisionEnter(Collision other)
    {
        EvaluateGroundContact(other);
    }

    private void OnCollisionStay(Collision other)
    {
        EvaluateGroundContact(other);
    }

    private void EvaluateGroundContact(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _groundNormal = other.GetContact(0).normal;
        }
    }
}