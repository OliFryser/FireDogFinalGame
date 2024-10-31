using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _movementSpeed = 50.0f;
    private Vector2 _direction;

    public Vector2 PreviousDirection;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    public bool FacingRight;

    private bool IsMoving => _direction.sqrMagnitude > 0.01f;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = _spriteRenderer.GetComponent<Animator>();
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
        FacingRight = _direction.x > 0;
        _spriteRenderer.flipX = FacingRight;
    }

    // Used by input system
    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();
    }
}
