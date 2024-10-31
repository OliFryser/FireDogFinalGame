using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _playerTransform;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _movementSpeed = 2.0f;

    [SerializeField]
    private float _sightDistance = 5.0f;

    private Animator _animator;

    private Vector2 _playerDirection;

    private Vector2 _enemyDirection;

    public bool FacingRight;

    void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _playerDirection = (_playerTransform.position - transform.position).normalized;

        if (!PlayerIsInLineOfSight())
        {
            _enemyDirection = Vector2.zero;
            UpdateAnimator();
            return;
        }

        _enemyDirection = _playerDirection;

        transform.Translate(_movementSpeed * Time.deltaTime * _enemyDirection);
        FlipSprite();

        UpdateAnimator();
    }

    bool PlayerIsInLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerDirection);
        Debug.Log($"Hit: {hit.collider.tag}");
        if (!hit)
            return false;

        Debug.DrawRay(transform.position, _playerDirection);
        return hit.collider.CompareTag("Player") && hit.distance < _sightDistance;
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Speed", _enemyDirection.sqrMagnitude);
        if (_enemyDirection.sqrMagnitude > 0.01f)
        {
            _animator.SetFloat("Horizontal", _playerDirection.x);
            _animator.SetFloat("Vertical", _playerDirection.y);
        }
    }


    private void FlipSprite()
    {
        if (Math.Abs(_playerDirection.x) < 0.01f) return;
        FacingRight = _playerDirection.x > 0;
        _spriteRenderer.flipX = FacingRight;
    }
}
