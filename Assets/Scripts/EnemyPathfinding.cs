using System;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    //private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _movementSpeed = 2.0f;

    [SerializeField]
    private float _sightDistance = 5.0f;

    private Animator _animator;

    private Vector2 _direction;
    
    public bool FacingRight;

    void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!PlayerIsInLineOfSight()) return;

        _direction = (_playerTransform.position - transform.position).normalized;

        transform.Translate(_movementSpeed * Time.deltaTime * _direction);

        FlipSprite();

        UpdateAnimator();
    }

    bool PlayerIsInLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (_playerTransform.position - transform.position).normalized);
        if (!hit)
            return false;

        Debug.DrawRay(transform.position, (_playerTransform.position - transform.position).normalized);
        return hit.collider.CompareTag("Player") && hit.distance < _sightDistance;
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Speed", _direction.sqrMagnitude);
        if (_direction.sqrMagnitude > 0.01f)
        {
            _animator.SetFloat("Horizontal", _direction.x);
            _animator.SetFloat("Vertical", _direction.y);
        }
    }


    private void FlipSprite()
    {
        if (Math.Abs(_direction.x) < 0.01f) return;
        FacingRight = _direction.x > 0;
        _spriteRenderer.flipX = FacingRight;
    }
}
