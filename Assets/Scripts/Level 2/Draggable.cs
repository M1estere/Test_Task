using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Draggable : MonoBehaviour
{
    [SerializeField] private Material _outlineMaterial;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.materials[1].SetFloat("_Scale", 0);
    }

    private void OnMouseOver()
    {
        if (LevelController.Instance.CanDrag == false) return;

        _meshRenderer.materials[1].SetFloat("_Scale", .03f);
    }
    private void OnMouseExit() 
    {
        if (LevelController.Instance.CanDrag == false) return;

        _meshRenderer.materials[1].SetFloat("_Scale", 0); 
    }
}
