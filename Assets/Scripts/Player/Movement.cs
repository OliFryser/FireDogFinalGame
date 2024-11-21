using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float _movementSpeed => _playerStats.MovementSpeed;
    public bool canMove = true;

    [Header("Animation scaling")]
    [SerializeField]
    private float _animationScaling = .03f;

    [SerializeField]
    private float _idleAnimationScaling = .015f;

    [Header("Dodging")]
    [SerializeField]
    private float _totalDodgeDistance;

    [SerializeField, Range(1, 15)]
    private float _dogdeSpeedScalar;

    [SerializeField]
    private float _invincibilityTime;

    private float _currentDodgeDistance;

    private Vector2 _direction;

    public Vector2 PreviousDirection;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private Weapon _playerWeapon;
    private bool _isPushed;
    private float _totalPushDistance;
    private float _currentPushDistance = 0;
    private bool _dodging;
    private PlayerHitDetection _hitDetection;
    private PlayerStats _playerStats;

    public bool IsMoving => _direction.sqrMagnitude > 0.01f;

    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerWeapon = GetComponent<Weapon>();
        _hitDetection = GetComponent<PlayerHitDetection>();
    }

    void FixedUpdate()
    {
        if (!canMove) return;

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
                if (_dodging)
                {
                    DoDodgeRoll();
                }
                else
                {
                    _rigidBody2D.AddForce(_direction * _movementSpeed);
                }
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
        if (!canMove) return;
        _direction = input.Get<Vector2>();
    }

    void OnDodgeRoll(InputValue _)
    {
        _dodging = true;
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


    void DoDodgeRoll()
    {
        _animator.SetTrigger("Dodge");
        StartCoroutine(_hitDetection.MakeInvincible(_invincibilityTime));
        while (_currentDodgeDistance < _totalDodgeDistance)
        {
            _rigidBody2D.AddForce(PreviousDirection * (_movementSpeed * _dogdeSpeedScalar));
            _currentDodgeDistance += _movementSpeed * _dogdeSpeedScalar * Time.fixedDeltaTime;
        }
        _currentDodgeDistance = 0;
        _dodging = false;
    }
}
