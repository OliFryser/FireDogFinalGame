using FMODUnity;
using System;
using System.Collections;
using Lib;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float _movementSpeed => _playerStats.MovementSpeed;

    [SerializeField]
    private float _knockBackSpeed = 5.0f;

    [Header("Animation scaling")]
    [SerializeField]
    private float _animationScaling = .03f;

    [SerializeField]
    private float _idleAnimationScaling = .015f;

    [Header("Dodging")]
    private float _totalDodgeDistance => _playerStats.DodgeDistance;

    [SerializeField, Range(1, 15)]
    private float _dogdeSpeedScalar;

    [SerializeField]
    private GameObject _dodgeHitBox;

    private float _dodgeCooldown => _playerStats.DodgeCooldown;

    private bool _dodgeOnCooldown;


    [SerializeField]
    private float _invincibilityTime;

    private float _currentDodgeDistance;

    private Vector2 _direction;
    private Vector2 _pushDirection;

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
    private InvincibilityManager _invincibilityManager;

    private bool _isHorizontal;
    
    [SerializeField]
    private GameObject _verticalShadow;
    [SerializeField]
    private GameObject _horizontalShadow;

    public bool IsMoving => _direction.sqrMagnitude > 0.01f;

    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerWeapon = GetComponent<Weapon>();
        _hitDetection = GetComponent<PlayerHitDetection>();
        _invincibilityManager = GetComponent<InvincibilityManager>();
    }

    void FixedUpdate()
    {
        if (_isPushed)
        {
            if (_currentPushDistance < _totalPushDistance)
            {
                _rigidBody2D.AddForce(_movementSpeed * _knockBackSpeed * _pushDirection);
                _currentPushDistance += _movementSpeed * _knockBackSpeed * Time.fixedDeltaTime;
            }
            else
            {
                StopPush();
            }

            return;
        }

        if (_playerWeapon.IsAttacking && _playerWeapon.HeavyAttack)
            return;

        if (IsMoving)
            PreviousDirection = _direction;

        if (_dodging && !_dodgeOnCooldown)
        {
            DoDodgeRoll();
        }
        else
        {
            _rigidBody2D.AddForce(_direction * _movementSpeed);
        }
        FlipSprite();
        UpdateAnimator();
        UpdateShadow();
    }

    private void UpdateShadow()
    {
        var isHorizontal = Utils.IsHorizontal(PreviousDirection);
        if (isHorizontal == _isHorizontal) return;
        _isHorizontal = isHorizontal;
        if (_isHorizontal)
        {
            _horizontalShadow.SetActive(true);
            _verticalShadow.SetActive(false);
        }
        else
        {
            _horizontalShadow.SetActive(false);
            _verticalShadow.SetActive(true);
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

    public void GetPushed(Vector2 enemyDirection)
    {
        if (_invincibilityManager != null && _invincibilityManager.IsInvincible) return;

        _isPushed = true;
        _pushDirection = (enemyDirection - _direction).normalized;
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
        
        if (_currentDodgeDistance == 0)
        {   
            _animator.SetTrigger("Dodge");
            RuntimeManager.PlayOneShot("event:/Player/Dodge");
            if(_playerStats.BowlingChampion){
                GameObject hitBox = Instantiate(_dodgeHitBox, transform);
                StartCoroutine(DestroyDodgeHitBoxafterDelay(hitBox, (_invincibilityTime+0.1f)));
                StartCoroutine(_hitDetection.MakeInvincible(_invincibilityTime+0.1f));
            }
            else {
                StartCoroutine(_hitDetection.MakeInvincible(_invincibilityTime));
            }
            
        }
        if (_currentDodgeDistance < _totalDodgeDistance)
        {
            _rigidBody2D.AddForce(PreviousDirection * (_movementSpeed * _dogdeSpeedScalar));
            _currentDodgeDistance += _movementSpeed * _dogdeSpeedScalar * Time.fixedDeltaTime;
        }
        else
        {
            _currentDodgeDistance = 0;
            _dodging = false;
            StartCoroutine(DodgeOnCooldown(_dodgeCooldown));
        }
    }

    IEnumerator DodgeOnCooldown(float cooldown)
    {
        _dodgeOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        _dodgeOnCooldown = false;
    }

    IEnumerator DestroyDodgeHitBoxafterDelay(GameObject hitBox, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(hitBox);
    }

    #region Input System
    void OnMove(InputValue input)
    {
        Vector2 rawInput = input.Get<Vector2>();
        if (rawInput.magnitude > 0.6)
            _direction = rawInput.normalized;
        else
            _direction = Vector2.zero;
    }

    void OnDodgeRoll(InputValue _)
    {
        if (!_dodgeOnCooldown)
            _dodging = true;
    }
    #endregion
}
