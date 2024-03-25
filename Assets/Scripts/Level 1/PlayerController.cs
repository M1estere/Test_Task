using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour 
{
    private int _roadIndex = 2;

    private LevelOneController _controller;

    private void Awake()
    {
        _controller = FindObjectOfType<LevelOneController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _roadIndex > 0)
        {
            _roadIndex--;
            transform.DOMoveX(_controller.Positions[_roadIndex], .1f);
        }

        if (Input.GetKeyDown(KeyCode.D) && _roadIndex < 4) 
        {
            _roadIndex++;
            transform.DOMoveX(_controller.Positions[_roadIndex], .1f);
        }
    }
}
