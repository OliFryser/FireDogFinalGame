using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float _movementSpeed => _playerStats.MovementSpeed;

    [SerializeField]
    private float _animationScaling = .03f;
    [SerializeField]
    private float _idleAnimationScaling = .015f;

    private Vector2 _direction;

    public Vector2 PreviousDirection;
    public Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private Weapon _playerWeapon;
    private bool _isPushed;
    private float _totalPushDistance;
    private float _currentPushDistance = 0;

    private PlayerStats _playerStats;

    public bool IsMoving => _direction.sqrMagnitude > 0.01f;

    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerWeapon = GetComponent<Weapon>();
    }

    void FixedUpdate()
    {
        if (_isPushed)
        {
            if (_currentPushDistance < _totalPushDistance)
            {
                _rigidBody2D.AddForce(_movementSpeed * 5 * _direction);
                _currentPushDistance += _movementSpeed * 5 * Time.fixedDeltaTime;
            }
            else
            {
                StopPush();
            }
        }
        else
        {
            if (!_playerWeapon.IsAttacking)
            {
                if (IsMoving)
                    PreviousDirection = _direction;

                _rigidBody2D.AddForce(_direction * _movementSpeed);

                FlipSprite();
                UpdateAnimator();
            }
        }
    }

    private void UpdateAnimator()
    {
        float newAnimationSpeed = _movementSpeed * _animationScaling;
        float newIdleSpeed = _movementSpeed * _idleAnimationScaling;

        _animator.SetFloat("Speed", _direction.sqrMagnitude);
        if (_animator.GetFloat("Animation Speed") != newAnimationSpeed)
            _animator.SetFloat("Animation Speed", newAnimationSpeed);

        if (_animator.GetFloat("Idle Speed") != newIdleSpeed)
            _animator.SetFloat("Idle Speed", newIdleSpeed);

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
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

    }

    // Used by input system
    void OnMove(InputValue input)
    {
        _direction = input.Get<Vector2>();
    }


    public void GetPushed(Vector2 enemyDirection)
    {
        _isPushed = true;
        _direction = (enemyDirection - _direction).normalized;
        _totalPushDistance = _playerStats.PlayerPushBack;
    }

    public void StopPush()
    {
        _isPushed = false;
        _direction = Vector2.zero;
        _totalPushDistance = 0;
        _currentPushDistance = 0;
    }
}
