using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [Space(5)]

    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _fastSpeed;
    [Space(5)]

    [SerializeField] private float _movementTime;
    [SerializeField] private float _rotationAmount;
    [SerializeField] private Vector3 _zoomAmount;

    private Vector3 _newPosition;
    private Quaternion _newRotation;
    private Vector3 _newZoom;

    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;

    private Vector3 _rotateStartPosition;
    private Vector3 _rotateCurrentPosition;

    private float _movementSpeed;

    private void Start()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
        _newZoom = _cameraTransform.localPosition;
    }

    private void LateUpdate()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _newZoom += Input.mouseScrollDelta.y * _zoomAmount;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                _dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                _dragCurrentPosition = ray.GetPoint(entry);

                _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            _rotateCurrentPosition = Input.mousePosition;

            Vector3 diff = _rotateStartPosition - _rotateCurrentPosition;

            _rotateStartPosition = _rotateCurrentPosition;

            _newRotation *= Quaternion.Euler(Vector3.up * (-diff.x / 5));
            // _newRotation *= Quaternion.Euler(Vector3.right * (diff.y / 5));
        }
    }

    private void HandleMovementInput()
    {
        /*if (Input.GetKey(KeyCode.LeftShift)) _movementSpeed = _fastSpeed;
        else _movementSpeed = _normalSpeed;*/

        _movementSpeed = Input.GetKey(KeyCode.LeftShift) ? _fastSpeed : _normalSpeed;

        _newPosition += (transform.forward * (Input.GetAxis("Vertical") * _movementSpeed));
        _newPosition += (transform.right * (Input.GetAxis("Horizontal") * _movementSpeed));

        /*if (Input.GetKey(KeyCode.W) ||  Input.GetKey(KeyCode.UpArrow))
        {
            _newPosition += (transform.forward * _movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += (transform.forward * -_movementSpeed));
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += (transform.right * -_movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += (transform.right * _movementSpeed);
        }*/

        if (Input.GetKey(KeyCode.Q)) _newRotation *= Quaternion.Euler(Vector3.up * -_rotationAmount);
        if (Input.GetKey(KeyCode.E)) _newRotation *= Quaternion.Euler(Vector3.up * _rotationAmount);

        if (Input.GetKey(KeyCode.R)) _newZoom += _zoomAmount;
        if (Input.GetKey(KeyCode.F)) _newZoom += -_zoomAmount;

        transform.SetPositionAndRotation(
            Vector3.Lerp(transform.position, _newPosition, _movementTime * Time.deltaTime), 
            Quaternion.Lerp(transform.rotation, _newRotation, _movementTime * Time.deltaTime));

        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _newZoom, _movementTime * Time.deltaTime);
    }
}
