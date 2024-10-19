using UnityEngine;

public class Cube : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] ForceMode _forceMode;
    private Vector3 _startPosition;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _startPosition = transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = _startPosition;
        }
    }
    public void Throw(Vector3 direction)
    {
        rigidBody.AddForce(direction, _forceMode);
    }
}