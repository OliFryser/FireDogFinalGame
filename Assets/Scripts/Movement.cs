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

    public bool FacingUp;

    private bool _isMoving => _direction.sqrMagnitude > 0.01f;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = _spriteRenderer.GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        if (_isMoving) 
            PreviousDirection = _direction;

        
        _rigidBody2D.AddForce(_direction * _movementSpeed);

        IsFacingUp();
        Debug.Log("Facing up?: " + FacingUp.ToString());
        FlipSprite();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Speed", _direction.sqrMagnitude);
        if (_isMoving)
        {
            _animator.SetFloat("Horizontal", _direction.x);
            _animator.SetFloat("Vertical", _direction.y);
        }
    }

    private void FlipSprite()
    {
        if (Math.Abs(_direction.x) < 0.01f) return;
        FacingRight = _direction.x > 0;
        //Debug.Log ("Direction x: " + _direction.x.ToString());
        //Debug.Log("Direction y: " + _direction.y.ToString());
        _spriteRenderer.flipX = FacingRight;
    }

    // Used by input system
    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();
        //Debug.Log ("Direction: " + _direction.ToString());
        //Debug.Log ("Direction x: " + _direction.x.ToString());
        //Debug.Log("Direction y: " + _direction.y.ToString());
    }

    void IsFacingUp() {
        if (_direction.y > 0) {
            FacingUp = true;
        } 
        else if (_direction.y < 0 ) {
            FacingUp = false;
        }
    }
}
