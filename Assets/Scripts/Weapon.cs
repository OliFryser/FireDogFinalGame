using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class Weapon : MonoBehaviour
{
    private Movement _playerMovement;

    public bool HeavyAttack = false;
    public bool IsAttacking = false;

    [SerializeField]
    private GameObject _lightHitBoxPrefabDown;

    [SerializeField]
    private GameObject _lightHitBoxPrefabUp;

    [SerializeField]
    private GameObject _lightHitBoxPrefabSide;

    [SerializeField]
    private GameObject _heavyHitBoxPrefabDown;

    [SerializeField]
    private GameObject _heavyHitBoxPrefabSide;

    [SerializeField]
    private GameObject _heavyHitBoxPrefabUp;

    private GameObject _finalHitBox;

    private Animator _animator;

    [SerializeField]
    private float _hitBoxOffset = 0.75f;

    [SerializeField]
    private float _hitBoxDestroyDelayLight = 0.55f;

    [SerializeField]
    private float _hitBoxDestroyDelayHeavy = 0.9f;

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
            SpawnAttackHitBox(_lightAttack);
            DoLightAttack();
            _lightAttack = false;
        }
        if (_heavyAttack)
        {   
            _isAttacking = true;
            StartCoroutine(SpawnHitBoxAfterDelay(0.6f));
            DoHeavyAttack();
            _heavyAttack = false;
        }

        }
    }

    private void SpawnAttackHitBox(bool isLight)
    {
        Vector3 direction;
        if (Math.Abs(_playerMovement.PreviousDirection.x) >= Math.Abs(_playerMovement.PreviousDirection.y))
        {
            // facing either left or right
            if (_playerMovement.PreviousDirection.x > 0) {
                direction = Vector3.right;
            }
            else {
                direction = Vector3.left;
            }
            if (isLight)
                _finalHitBox = _lightHitBoxPrefabSide;
            else {
                _finalHitBox = _heavyHitBoxPrefabSide;
                _hitBoxOffset = 0.8f;
            }
        }
        else
        {
            // facing either up or down
            if (_playerMovement.PreviousDirection.y > 0){
                direction = Vector3.up;
                if(isLight) {
                    _hitBoxOffset = 0.35f;
                    _finalHitBox = _lightHitBoxPrefabUp;
                }
                else {
                    _hitBoxOffset = 0.9f;
                    _finalHitBox = _heavyHitBoxPrefabUp;
                }
            }
                    
            else {
                direction = Vector3.down;
                if(isLight) {
                    _hitBoxOffset = 0.9f;
                    _finalHitBox = _lightHitBoxPrefabDown;
                }
                else {
                    _hitBoxOffset = 1.3f;
                    _finalHitBox = _heavyHitBoxPrefabDown;
                }     
            }
                
        }

        _playerMovement._rigidBody2D.linearVelocity = Vector2.zero;
        
        GameObject hitBox = Instantiate(_finalHitBox, transform.position + direction * _hitBoxOffset, quaternion.identity);
        if (isLight) {

            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayLight));
        }
        else
            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayHeavy));

        _hitBoxOffset = 0.45f;

    }

    void OnLightAttack(InputValue _)
    {
        _lightAttack = true;
    }

    void OnHeavyAttack(InputValue _)
    {
        HeavyAttack = true;
    }

    IEnumerator DestroyAfterDelay(GameObject hitBox, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(hitBox);
        HeavyAttack = false;
        IsAttacking = false;
    }

    void DoLightAttack()
    {
        IsAttacking = true;
        RuntimeManager.PlayOneShot(MeleeLightSwing);
        _animator.SetTrigger("LightAttack");
    }

    void DoHeavyAttack()
    {
        _animator.SetTrigger("HeavyAttack");
    }

    IEnumerator SpawnHitBoxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnAttackHitBox(_lightAttack);
    }

}
