using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    const float AGENT_DRIFT = 0.0001f;
    private Transform _playerTransform;

    [SerializeField]
    protected float _movementSpeed = 50.0f;

    [SerializeField, Range(0.01f, 2f)]
    private float _animationScaling = .03f;

    [SerializeField]
    private float _sightDistance = 5.0f;

    private float _knockBackSpeed => _playerStats.EnemyPushBackSpeed;

    public bool IsPushedBack = false;

    private Animator _animator;

    private Vector2 _directionToPlayer;

    private Vector2 _direction;

    private float _totalPushDistance;

    private float _currentPushDistance;

    private bool _isStunned;

    private float _totalStunTimer;

    private float _currentStunTimer;

    private PlayerStats _playerStats;

    [SerializeField]
    private CollisionSettings _verticalEnemyCollision;

    [SerializeField]
    private CollisionSettings _horizontalEnemyCollsion;

    private BoxCollider2D _boxCollider;

    protected NavMeshAgent _navMeshAgent;

    private LayerMask _mask;

    private EnemyHitDetection _hitDetection;

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    protected virtual void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
        _playerStats = FindAnyObjectByType<PlayerStats>();

        _boxCollider = GetComponent<BoxCollider2D>();
        UpdateCollider(_verticalEnemyCollision);

        _direction = Vector2.down;
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
        _hitDetection = GetComponent<EnemyHitDetection>();

        _navMeshAgent.speed = _movementSpeed;
        _mask = LayerMask.GetMask("Ignore Raycast", "Dust", "Enemy");
    }

    protected virtual void Update()
    {
        _directionToPlayer = (_playerTransform.position - transform.position).normalized;

        if (IsPushedBack)
        {
            if (_currentPushDistance < _totalPushDistance)
            {
                _navMeshAgent.velocity = Vector2.zero;
                transform.Translate(_knockBackSpeed * Time.deltaTime * -_directionToPlayer);
                _currentPushDistance += _knockBackSpeed * Time.deltaTime;
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
                _currentStunTimer += Time.deltaTime;
                _navMeshAgent.velocity = Vector2.zero;
            }
            else
                StopStun();
        }
        else
        {
            if (!PlayerIsInLineOfSight())
            {
                _direction = Vector2.zero;
                _navMeshAgent.velocity = _direction;
                UpdateAnimator();
                return;
            }

            SetDestination(_playerTransform);
            _direction = _navMeshAgent.velocity.normalized;


            if (Utils.IsHorizontal(_direction))
                UpdateCollider(_horizontalEnemyCollsion);
            else
                UpdateCollider(_verticalEnemyCollision);

            FlipSprite();
            UpdateAnimator();
        }
    }

    private void SetDestination(Transform playerTransform)
    {
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < AGENT_DRIFT)
        {
            var driftPos = playerTransform.position + new Vector3(AGENT_DRIFT, 0f, 0f);
            _navMeshAgent.SetDestination(driftPos);
        }
        else
            _navMeshAgent.SetDestination(playerTransform.position);
    }

    private void UpdateCollider(CollisionSettings settings)
    {
        _boxCollider.size = settings.Size;
        _boxCollider.offset = settings.Offset;
    }

    bool PlayerIsInLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _directionToPlayer, _sightDistance, layerMask: ~_mask);
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
        _animator.SetFloat("Speed", _direction.magnitude);
        if (_direction.magnitude > 0.01f)
        {
            _animator.SetFloat("Horizontal", _direction.x);
            _animator.SetFloat("Vertical", _direction.y);
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


    public void GetPushedBack(float distance, float stun)
    {
        IsPushedBack = true;
        _direction = (gameObject.transform.position - _playerTransform.transform.position).normalized;
        _totalPushDistance = distance;
        _isStunned = true;
        _totalStunTimer = stun;
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
        return _direction;
    }

    public Vector2 GetDirectionToPlayer()
        => (transform.position - _playerTransform.position).normalized;


// Doesn't work properly yet.
    private void OnCollisionEnter2D(Collision2D other){
        if (IsPushedBack && _playerStats.BaseballConnoisseur){
            IsPushedBack = false;
            _totalPushDistance = 0;
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Untagged")){
                _hitDetection._health -= 20;
            }
        }
        
    }

    [Serializable]
    struct CollisionSettings
    {
        public Vector2 Offset;
        public Vector2 Size;
    }
}
