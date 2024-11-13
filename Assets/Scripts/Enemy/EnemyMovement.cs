using System;
using FMODUnity;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField]
    private float _movementSpeed = 50.0f;

    [SerializeField]
    private float _animationScaling = .03f;

    [SerializeField]
    private float _sightDistance = 5.0f;
    
    [SerializeField]
    private float _knockBackSpeed;

    public bool IsPushedBack = false;

    private Animator _animator;

    private PlayerStats _playerStats;

    private Vector2 _directionToPlayer;

    private Vector2 _enemyDirection;

    private float _totalPushDistance;

    private float _currentPushDistance;

    private bool _isStunned;

    private float _totalStunTimer;

    private float _currentStunTimer;

    [SerializeField]
    private CollisionSettings _verticalEnemyCollision;

    [SerializeField]
    private CollisionSettings _horizontalEnemyCollsion;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;

    void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
        _rigidbody = GetComponent<Rigidbody2D>();

        _boxCollider = GetComponent<BoxCollider2D>();
        UpdateCollider(_verticalEnemyCollision);

        _playerStats = FindAnyObjectByType<PlayerStats>();

        _enemyDirection = Vector2.down;
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Horizontal", _enemyDirection.x);
        _animator.SetFloat("Vertical", _enemyDirection.y);
    }

    void Update()
    {
        if (_isStunned)
        {
            if (_currentStunTimer < _totalStunTimer)
                _currentStunTimer += Time.deltaTime;
            else
                StopStun();
        }
    }

    void FixedUpdate()
    {
        if (IsPushedBack)
        {
            if (_currentPushDistance < _totalPushDistance)
            {
                _rigidbody.AddForce(_knockBackSpeed * _enemyDirection);
                _currentPushDistance += _knockBackSpeed;
            }
            else
            {
                StopPush();
            }
        }
        else if (_isStunned && _currentStunTimer < _totalStunTimer) {
            //Makes sure the enemy doesn't move while stunned.
            //_rigidbody.AddForce(_movementSpeed * Vector2.zero);
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

            if (Utils.IsHorizontal(_enemyDirection))
                UpdateCollider(_horizontalEnemyCollsion);
            else
                UpdateCollider(_verticalEnemyCollision);

            _rigidbody.AddForce(_movementSpeed * _enemyDirection);
            FlipSprite();

            UpdateAnimator();
        }
    }

    private void UpdateCollider(CollisionSettings settings)
    {
        _boxCollider.size = settings.Size;
        _boxCollider.offset = settings.Offset;
    }

    bool PlayerIsInLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionToPlayer);
        if (!hit)
            return false;

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
        _totalPushDistance = distance * _movementSpeed;
        if (heavy)
        {
            _isStunned = true;
            _totalStunTimer = _playerStats.EnemyStunDuration;
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

    [Serializable]
    struct CollisionSettings
    {
        public Vector2 Offset;
        public Vector2 Size;
    }
}
