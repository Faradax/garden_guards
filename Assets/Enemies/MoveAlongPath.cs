using PathCreation;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{

    public PathCreator pathCreator;
    public float speed;
    
    private VertexPath _path;
    private Vector3 _desiredDirection;
    private Rigidbody _rigidbody;

    void Start()
    {
        this._path = pathCreator.path;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distanceAlongPath = _path.GetClosestDistanceAlongPath(currentPosition);
        // We can only see a short distance ahead, yet we can see plenty there that needs to be done.
        Vector3 whereTo = _path.GetPointAtDistance(distanceAlongPath + .2f);

        Vector3 difference = (whereTo - currentPosition);
        _desiredDirection = new Vector3(difference.x, 0, difference.z).normalized;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _desiredDirection * speed;
    }
}
