using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField]
    private float _movementSpeed = 2.0f;

    [SerializeField]
    private float _animationScaling = .75f;

    [SerializeField]
    private float _sightDistance = 5.0f;

    public bool IsPushedBack = false;

    private Animator _animator;

    private Vector2 _directionToPlayer;

    private Vector2 _enemyDirection;

    private float _totalPushDistance;

    private float _currentPushDistance;

    private bool _isStunned;

    private float _totalStunTimer;

    private float _currentStunTimer;



    void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
        _animator = GetComponent<Animator>();

        _enemyDirection = Vector2.down;
        _animator.SetFloat("Horizontal", _enemyDirection.x);
        _animator.SetFloat("Vertical", _enemyDirection.y);
    }

    void Update()
    {
        if (IsPushedBack)
        {
            if (_currentPushDistance < _totalPushDistance)
            {
                transform.Translate(_movementSpeed * Time.deltaTime * _enemyDirection);
                _currentPushDistance += Time.deltaTime * _movementSpeed;
            }
            else
            {
                StopPush();
            }
        }
        else if (_isStunned)
        {
            if (_currentStunTimer < _totalStunTimer)
            {
                _enemyDirection = Vector2.zero;
                transform.Translate(_movementSpeed * Time.deltaTime * _enemyDirection);
                _currentStunTimer += Time.deltaTime;
            }
            else
            {
                StopStun();
            }
        }
        else
        {
            _directionToPlayer = (_playerTransform.position - transform.position).normalized;

            if (!PlayerIsInLineOfSight())
            {
                _enemyDirection = Vector2.zero;
                UpdateAnimator();
                return;
            }

            _enemyDirection = _directionToPlayer;

            transform.Translate(_movementSpeed * Time.deltaTime * _enemyDirection);
            FlipSprite();

            UpdateAnimator();
        }
    }

    bool PlayerIsInLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionToPlayer);
        if (!hit)
            return false;

        Debug.DrawRay(transform.position, _directionToPlayer);
        return hit.collider.CompareTag("Player") && hit.distance < _sightDistance;
    }

    private void UpdateAnimator()
    {
        float newAnimationSpeed = _movementSpeed * _animationScaling;
        if (_animator.GetFloat("Animation Speed") != newAnimationSpeed)
        {
            _animator.SetFloat("Animation Speed", newAnimationSpeed);
        }
        _animator.SetFloat("Speed", _enemyDirection.sqrMagnitude);
        if (_enemyDirection.sqrMagnitude > 0.01f)
        {
            _animator.SetFloat("Horizontal", _enemyDirection.x);
            _animator.SetFloat("Vertical", _enemyDirection.y);
        }
    }


    private void FlipSprite()
    {
        if (Math.Abs(_directionToPlayer.x) < 0.01f) return;
        if (_directionToPlayer.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }


    public void GetPushedBack(float distance, bool heavy)
    {
        IsPushedBack = true;
        _enemyDirection = (gameObject.transform.position - _playerTransform.transform.position).normalized;
        _totalPushDistance = distance;
        if (heavy)
        {
            _isStunned = true;
            _totalPushDistance += 2.0f - Vector2.Distance(_playerTransform.transform.position, gameObject.transform.position);
        }
    }

    public void StopPush()
    {
        IsPushedBack = false;
        _totalPushDistance = 0;
        _currentPushDistance = 0;
    }

    public void StopStun()
    {
        _isStunned = false;
        _totalStunTimer = 0;
        _currentStunTimer = 0;
    }

    public Vector2 GetEnemyDirection()
    {
        return _enemyDirection;
    }
}
