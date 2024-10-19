using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] LayerMask _groundLayer, _throwLayer;
    [SerializeField] float _hitRadius = 1f;
    [SerializeField] float _hitForce = 5f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _groundLayer))
        {
            var colliders = Physics.OverlapSphere(hitInfo.point, _hitRadius, _throwLayer);
            foreach (var collider in colliders)
            {
                collider.attachedRigidbody.AddForce((collider.transform.position - hitInfo.point) * _hitForce, ForceMode.Impulse);
            }
        }
    }

}
