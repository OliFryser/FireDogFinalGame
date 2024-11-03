using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class Weapon : MonoBehaviour
{
    private Movement _playerMovement;

    public bool _heavyAttack = false;
    public bool _isAttacking = false;

    [SerializeField]
    private GameObject _lightHitBoxPrefab;

    [SerializeField]
    private GameObject _heavyHitBoxPrefab;

    private Animator _animator;

    [SerializeField]
    private float _hitBoxOffset = 0.75f;

    [SerializeField]
    private float _hitBoxDestroyDelayLight = 0.55f;

    [SerializeField]
    private float _hitBoxDestroyDelayHeavy = 1.5f;

    private bool _lightAttack = false;

    [SerializeField]
    EventReference MeleeLightSwing;

    void Start()
    {
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>(); 
    }

    private void FixedUpdate()
    {
        if (!_isAttacking) {
        if (_lightAttack)
        {
            _isAttacking = true;
            LightAttack();
            SpawnAttackHitBox(_lightHitBoxPrefab, _lightAttack);
            _lightAttack = false;
        }
        if (_heavyAttack)
        {   
            _isAttacking = true;
            HeavyAttack();
            SpawnAttackHitBox(_heavyHitBoxPrefab, _lightAttack);
            _heavyAttack = false;
        }
        }
    }

    private void SpawnAttackHitBox(GameObject hitBoxPrefab, bool isLight)
    {
        Vector3 direction;
        if (Math.Abs(_playerMovement.PreviousDirection.x) >= Math.Abs(_playerMovement.PreviousDirection.y))
        {
            // facing either left or right
            if (_playerMovement.PreviousDirection.x > 0)
                direction = Vector3.right;
            else
                direction = Vector3.left;
        }
        else
        {
            // facing either up or down
            if (_playerMovement.PreviousDirection.y > 0)
                direction = Vector3.up;
            else
                direction = Vector3.down;
        }
        GameObject hitBox = Instantiate(hitBoxPrefab, transform.position + direction * _hitBoxOffset, quaternion.identity);
        if (isLight) {
            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayLight));
        } else
            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayHeavy));
        
    }

    void OnLightAttack(InputValue _)
    {
        _lightAttack = true;
    }

    void OnHeavyAttack(InputValue _)
    {
        _heavyAttack = true;
    }

    IEnumerator DestroyAfterDelay(GameObject hitBox, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(hitBox);
        _heavyAttack = false;
        _isAttacking = false;
    }

    void OnEnemyHit()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    void LightAttack() {
        _isAttacking = true;
        RuntimeManager.PlayOneShot(MeleeLightSwing);
        _animator.SetTrigger("LightAttack");
    }

    void HeavyAttack() {
        _animator.SetTrigger("HeavyAttack");
    }


}
