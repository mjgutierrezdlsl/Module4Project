using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _smoothTime = 0.3f;
    private Camera _camera;
    private Vector2 _velocity;
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        var targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
