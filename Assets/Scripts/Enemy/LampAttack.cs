using System;
using UnityEngine;

public class LampAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private bool _isAttacking;
    private Animator _animator;

    private void Awake()
    {
        _animator = FindAnyObjectByType<Animator>();
    }

    private void Update()
    {
        if (_isAttacking)
        {
            _animator.SetTrigger("Attack");
            _isAttacking = false;
        }

    }
}
