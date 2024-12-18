using System.Collections;
using UnityEngine;
using FMODUnity;
using Unity.Mathematics;

public class LampAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackRange;

    [Header("Attack Timings")]
    [SerializeField, Range(0, 2)]
    private float _chargeUpMultiplier;

    [SerializeField]
    private float _hitTime;

    [SerializeField]
    private float _attackCooldown;

    private bool _isOnCooldown;

    [Header("Attack Objects")]
    [SerializeField]
    private GameObject _attackRing;

    [SerializeField]
    private GameObject _attackHitBox;

    private bool _isAttacking;

    public bool IsAttacking { get => _isAttacking; }

    private Animator _animator;

    private Transform _playerTransform;

    private LayerMask _mask;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.SetFloat("Charge Up Speed", _chargeUpMultiplier);
        _playerTransform = FindAnyObjectByType<Movement>().transform;
        _mask = LayerMask.GetMask("Ignore Raycast", "Dust", "Enemy");
    }

    private void Update()
    {
        if (_isAttacking)
            return;
        if (PlayerIsWithinAttackRange() && !_isOnCooldown)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        _isOnCooldown = true;
        _isAttacking = true;
        _attackRing.SetActive(true);
        _animator.SetTrigger("Attack");
        RuntimeManager.PlayOneShotAttached("event:/Enemy/Lamp_buildup", this.gameObject);
    }

    // Called from the animator when the charge up finishes
    public void ReleaseAttack()
    {
        _attackRing.SetActive(false);
        StartCoroutine(FinishAttack());
        RuntimeManager.PlayOneShotAttached("event:/Enemy/Lamp_blast", this.gameObject);
    }

    private IEnumerator FinishAttack()
    {
        _attackHitBox.SetActive(true);
        _attackHitBox.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(_hitTime);
        _animator.SetTrigger("Attack Finished");
        _attackHitBox.SetActive(false);
        _isAttacking = false;
        yield return new WaitForSeconds(_attackCooldown);
        _isOnCooldown = false;
    }

    private bool PlayerIsWithinAttackRange()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(
                transform.position,
                _playerTransform.position - transform.position,
                _attackRange,
                ~_mask);
        if (!hit) return false;
        if (hit.collider.CompareTag("Player")) return true;
        return false;
    }
}
