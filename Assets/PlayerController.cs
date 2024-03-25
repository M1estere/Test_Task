using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour 
{
    private float _roadWidth = 4;

    private Rigidbody2D _rigidbody;

    private float[] _positions = new float[] { -8, -4, 0, 4, 8 };
    private int _roadIndex = 2;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _roadIndex > 0)
        {
            _roadIndex--;
            transform.DOMoveX(_positions[_roadIndex], .1f);
        }

        if (Input.GetKeyDown(KeyCode.D) && _roadIndex < 4) 
        {
            _roadIndex++;
            transform.DOMoveX(_positions[_roadIndex], .1f);
        }
    }
}
