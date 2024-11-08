using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] ParticleSystem _deathParticles;
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
    public void Die()
    {
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        StartCoroutine(DeathRoutine());
    }
    private IEnumerator DeathRoutine()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
