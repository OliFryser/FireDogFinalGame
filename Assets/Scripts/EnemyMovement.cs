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

    private Animator _animator;

    private Vector2 _directionToPlayer;

    private Vector2 _enemyDirection;

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
}
