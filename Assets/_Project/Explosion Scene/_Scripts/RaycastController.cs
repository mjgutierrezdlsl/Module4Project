using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    [SerializeField] LayerMask _hitMask;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _hitMask))
        {
            print($"{hitInfo.collider.name}: {hitInfo.point}");
            if (hitInfo.collider.TryGetComponent<Tank>(out var tank))
            {
                tank.Explode();
            }
        }
    }
}
