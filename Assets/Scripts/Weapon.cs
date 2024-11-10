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
    private HitBox _lightHitBoxPrefabDown;

    [SerializeField]
    private HitBox _lightHitBoxPrefabUp;

    [SerializeField]
    private HitBox _lightHitBoxPrefabSide;

    [SerializeField]
    private HitBox _heavyHitBoxPrefabDown;

    [SerializeField]
    private HitBox _heavyHitBoxPrefabSide;

    [SerializeField]
    private HitBox _heavyHitBoxPrefabUp;

    private GameObject _finalHitBox;

    private Animator _animator;

    private InputLock _inputLocker;

    [SerializeField]
    private float _hitBoxOffset = 0.75f;

    [SerializeField]
    private float _hitBoxDestroyDelayLight = 0.55f;

    [SerializeField]
    private float _hitBoxDestroyDelayHeavy = 0.9f;

    [SerializeField]
    private float _lightCooldown;

    [SerializeField]
    private float _heavyCooldown;

    private bool _lightAttack = false;

    private bool _onCooldownLight = false;

    private bool _onCooldownHeavy = false;



    

    [SerializeField]
    EventReference MeleeLightSwing;

    void Start()
    {
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>(); 
        _inputLocker = GetComponent<InputLock>();
    }

    private void FixedUpdate()
    {
        if (!_isAttacking) {    
        if (_lightAttack && !_onCooldownLight)
        {
            StartCoroutine(CooldownTimer(_lightCooldown, _lightAttack));
             _inputLocker.LockInput();
            _isAttacking = true;
            SpawnAttackHitBox(_lightAttack);
            DoLightAttack();
             
        }
        if (_heavyAttack && !_onCooldownHeavy)
        {   
            StartCoroutine(CooldownTimer(_heavyCooldown, _lightAttack));
             _inputLocker.LockInput();
            _isAttacking = true;
            StartCoroutine(SpawnHitBoxAfterDelay(0.6f));
            DoHeavyAttack();
            
        }
        }
    }

    private void SpawnAttackHitBox(bool isLight)
    {
        HitBox attackHitBox;
        Vector3 direction;
        if (Math.Abs(_playerMovement.PreviousDirection.x) >= Math.Abs(_playerMovement.PreviousDirection.y))
        {
            // facing either left or right
            if (_playerMovement.PreviousDirection.x > 0)
                direction = Vector3.right;
            else
                direction = Vector3.left;

            if (isLight)
                attackHitBox = _lightHitBoxPrefabSide;
            else
                attackHitBox = _heavyHitBoxPrefabSide;
        }
        else
        {
            // facing either up or down
            if (_playerMovement.PreviousDirection.y > 0)
            {
                direction = Vector3.up;
                if (isLight)
                    attackHitBox = _lightHitBoxPrefabUp;
                else
                    attackHitBox = _heavyHitBoxPrefabUp;
            }

            else
            {
                direction = Vector3.down;
                if (isLight)
                    attackHitBox = _lightHitBoxPrefabDown;
                else
                    attackHitBox = _heavyHitBoxPrefabDown;
            }

        }
        _playerMovement._rigidBody2D.linearVelocity = Vector2.zero;
        
        GameObject hitBox = Instantiate(attackHitBox.Prefab, transform.position + direction * attackHitBox.Offset, quaternion.identity);
       
        if (isLight) {
            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayLight));
        } else
            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayHeavy));
        _hitBoxOffset = 0.45f;
    }

    

    

    IEnumerator DestroyAfterDelay(GameObject hitBox, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(hitBox);
        if (_lightAttack)
            StartCoroutine(DelayLightFinish());
        else {
            _inputLocker.UnlockInput();
            _heavyAttack = false;
            _isAttacking = false;
        }
        
    }

    void OnEnemyHit()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    void DoLightAttack() {
        _isAttacking = true;
        RuntimeManager.PlayOneShot(MeleeLightSwing);
        _animator.SetTrigger("LightAttack");
    }

    void DoHeavyAttack() {
        _animator.SetTrigger("HeavyAttack");
    }

    void HeavyAttackHold() {
    }

    IEnumerator SpawnHitBoxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnAttackHitBox(_lightAttack);
    }

    IEnumerator DelayLightFinish(){
        yield return new WaitForSeconds(0.15f);
        _lightAttack = false;
        _isAttacking = false;
        _inputLocker.UnlockInput();
    }


    IEnumerator CooldownTimer(float cd, bool isLight){
        if (isLight) {
            _onCooldownLight = true;
            yield return new WaitForSeconds(cd);
            _onCooldownLight = false;
        } else {
             _onCooldownHeavy = true;
            yield return new WaitForSeconds(cd);
            _onCooldownHeavy = false;
        }
       
    }

    #region Input System
    void OnLightAttack(InputValue _)
    {
        if (!_onCooldownLight)
            _lightAttack = true;
        }


    void OnHeavyAttack(InputValue _)
    {
        if (!_onCooldownHeavy)
            _heavyAttack = true;
            
    }

    #endregion

    [Serializable]
    struct HitBox
    {
        public GameObject Prefab;
        public float Offset;
    }

}
