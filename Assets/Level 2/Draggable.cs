using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Draggable : MonoBehaviour
{
    [SerializeField] private Material _outlineMaterial;

    private Vector3 screenPoint;
    private Vector3 offset;

    private CameraController _cameraController;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _cameraController = FindObjectOfType<CameraController>();

        _meshRenderer.materials[1].SetFloat("_Scale", 0);
    }

    private void OnMouseOver() => _meshRenderer.materials[1].SetFloat("_Scale", .03f);
    private void OnMouseExit() => _meshRenderer.materials[1].SetFloat("_Scale", 0);

    /*private void OnMouseDown()
    {
        print($"Clicked {gameObject.name}");
    }*/

    /*private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        _cameraController.enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        _cameraController.enabled = true;
    }*/
}
