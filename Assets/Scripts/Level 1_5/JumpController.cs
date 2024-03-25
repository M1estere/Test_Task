using DG.Tweening;
using System.Collections;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [Header("Jump Setup")]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDuration;

    private Rigidbody _rigidbody;
    private SonicController _controller;

    private void Awake()
    {
        _controller = GetComponent<SonicController>();
        _rigidbody = GetComponent<Rigidbody>();

        LockNecessaryConstraints();
    }

    private void LockNecessaryConstraints()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX |
                                 RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationZ;
    }

    public void Jump() => StartCoroutine(JumpCoroutine());
    private IEnumerator JumpCoroutine()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;

        transform.DOJump(new Vector3(transform.position.x, 0, transform.position.z), _jumpHeight, 1, _jumpDuration, false);

        _controller.ChangeJumping(true);

        yield return new WaitForSeconds(_jumpDuration);

        _controller.ChangeJumping(false);

        LockNecessaryConstraints();
    }
}
