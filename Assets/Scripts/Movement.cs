using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 50.0f;
    private Vector2 _direction;

    public Vector2 PreviousDirection;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    private bool IsMoving => _direction.sqrMagnitude > 0.01f;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        if (IsMoving)
            PreviousDirection = _direction;

        _rigidBody2D.AddForce(_direction * _movementSpeed);

        FlipSprite();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Speed", _direction.sqrMagnitude);
        if (IsMoving)
        {
            _animator.SetFloat("Horizontal", _direction.x);
            _animator.SetFloat("Vertical", _direction.y);
        }
    }

    private void FlipSprite()
    {
        if (Math.Abs(_direction.x) < 0.01f) return;
        if (_direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    // Used by input system
    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();
    }
}
