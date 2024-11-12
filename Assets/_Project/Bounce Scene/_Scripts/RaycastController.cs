using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] LayerMask _groundLayer, _throwLayer;
    [SerializeField] ForceMode _forceMode = ForceMode.Impulse;
    [SerializeField] float _hitRadius = 1f;
    [SerializeField] float _hitForce = 5f;
    RaycastHit hitInfo;
    Ray ray;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_forceMode == ForceMode.Impulse || _forceMode == ForceMode.VelocityChange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, _groundLayer))
                {
                    var colliders = Physics.OverlapSphere(hitInfo.point, _hitRadius, _throwLayer);
                    foreach (var collider in colliders)
                    {
                        collider.attachedRigidbody.AddForce((collider.transform.position - hitInfo.point) * _hitForce, _forceMode);
                    }
                }
            }
        }
        else if (_forceMode == ForceMode.Acceleration || _forceMode == ForceMode.Force)
        {
            if (Input.GetMouseButton(0))
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, _groundLayer))
                {
                    Debug.DrawLine(ray.origin, hitInfo.point, Color.cyan);
                    var colliders = Physics.OverlapSphere(hitInfo.point, _hitRadius, _throwLayer);
                    foreach (var collider in colliders)
                    {
                        Debug.DrawLine(collider.transform.position, hitInfo.point, Color.red);
                        collider.attachedRigidbody.AddForce((collider.transform.position - hitInfo.point) * _hitForce, _forceMode);
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(hitInfo.point, _hitRadius);
    }

}
