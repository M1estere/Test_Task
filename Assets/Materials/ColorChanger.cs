using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _strobeDuration = 2f;

    private Camera _camera;

    private void Awake() => _camera = FindObjectOfType<Camera>(); 

    public void Update() 
    {
        float t = Mathf.PingPong(Time.time / _strobeDuration, 1f);
        _camera.backgroundColor = _gradient.Evaluate(t);
    }
}
