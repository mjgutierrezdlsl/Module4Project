using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] LayerMask _hitMask;
    [SerializeField] float _timeToHit = 5f;

    RaycastHit2D _hitInfo;
    bool _canSeePlayer;
    float _viewTime;

    private void Update()
    {
        if (_player == null) { return; }
        var lookDirection = (_player.transform.position - transform.position).normalized;
        transform.up = lookDirection;

        _hitInfo = Physics2D.Raycast(transform.position, lookDirection, Mathf.Infinity, _hitMask);

        if (!_hitInfo) { return; }

        _canSeePlayer = _hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player");
        Debug.DrawLine(transform.position, _player.transform.position, _canSeePlayer ? Color.red : Color.green);

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _hitInfo.point);

        if (!_canSeePlayer)
        {
            _viewTime = 0f;
        }
        else
        {
            _viewTime += Time.deltaTime;
            if (_viewTime / _timeToHit >= 1f)
            {
                _player.Die();
                _viewTime = 0f;
            }
        }

        _lineRenderer.widthMultiplier = Mathf.Lerp(0f, 1f, _viewTime / _timeToHit);
    }
}
