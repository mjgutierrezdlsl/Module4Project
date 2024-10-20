using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] float _mouseSensitivity = 100f;
    [SerializeField] float _explosionForce = 5f;
    [SerializeField] float _viewDistance = 5f;
    [SerializeField] LayerMask _viewLayers;
    [SerializeField] Image _crosshair;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var lookDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * _mouseSensitivity * Time.deltaTime;
        xRotation -= lookDirection.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * lookDirection.x);


        var viewRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(viewRay, out var hitInfo, _viewDistance, _viewLayers))
        {
            Debug.DrawLine(viewRay.origin, hitInfo.point, Color.red);
            _crosshair.color = Color.red;
            if (Input.GetMouseButtonDown(0))
            {

                var rigidBody = hitInfo.collider.GetComponent<Rigidbody>();
                rigidBody.AddForce((hitInfo.point - transform.position) * _explosionForce, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.DrawRay(viewRay.origin, viewRay.direction * _viewDistance, Color.yellow);
            _crosshair.color = Color.white;
        }
    }
}
