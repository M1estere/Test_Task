using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(JumpController))]
public class SonicController : MonoBehaviour
{
    private float[] _positions = new float[]
    {
        3.5f, 1.75f, 0, -1.75f, -3.5f
    };

    private int _roadIndex = 2;

    private JumpController _jumpController;
    private Animator _animator;

    private bool _isJumping = false;

    public void ChangeJumping(bool state) => _isJumping = state;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _jumpController = GetComponent<JumpController>();
    }

    private void Update()
    {
        _animator.SetBool("Jump", _isJumping == true);

        if (Input.GetKeyDown(KeyCode.A) && _roadIndex > 0)
        {
            _roadIndex--;
            transform.DOMoveZ(_positions[_roadIndex], .1f);
        }

        if (Input.GetKeyDown(KeyCode.D) && _roadIndex < 4)
        {
            _roadIndex++;
            transform.DOMoveZ(_positions[_roadIndex], .1f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isJumping) return;
            _jumpController.Jump();
        }
    }
}
