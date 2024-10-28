using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _movementSpeed = 50.0f;
    public Vector2 _direction;
    private Rigidbody2D _rigidBody2D;

    public bool FacingRight;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidBody2D.AddForce(_direction * _movementSpeed);
        FlipSprite();
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
