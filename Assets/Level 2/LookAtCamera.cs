using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake() => _mainCamera = FindObjectOfType<Camera>();

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
    }
}