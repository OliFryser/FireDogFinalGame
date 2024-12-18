using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using FMODUnity;
using Player;

public class Weapon : MonoBehaviour
{
    private Movement _playerMovement;

    public bool IsAttacking;

    private Animator _animator;

    private InputLock _inputLocker;

    private PlayerStats _playerStats;

    [Header("Light Attack")]
    [SerializeField]
    private float _hitBoxDestroyDelayLight = 0.55f;

    [SerializeField]
    private float _lightAttackHitBoxDelay = 0.1f;

    [SerializeField]
    private float _lightCooldown;

    [SerializeField]
    private float _delayLightFinish;

    [SerializeField]
    private HitBox _lightHitBoxPrefabDown;

    [SerializeField]
    private HitBox _lightHitBoxPrefabUp;

    [SerializeField]
    private HitBox _lightHitBoxPrefabSide;

    [Header("Heavy Attack")]
    [SerializeField]
    private float _heavyAttackHitBoxDelay = 0.6f;

    [SerializeField]
    private float _hitBoxDestroyDelayHeavy = 0.9f;

    [SerializeField]
    private float _heavyCooldown;

    [SerializeField]
    private HitBox _heavyHitBoxPrefabDown;

    [SerializeField]
    private HitBox _heavyHitBoxPrefabSide;

    [SerializeField]
    private HitBox _heavyHitBoxPrefabUp;

    private bool _lightAttack;

    [HideInInspector]
    public bool HeavyAttack;

    private bool _onCooldownLight;

    private bool _onCooldownHeavy;


    [SerializeField]
    EventReference MeleeLightSwing;

    private void Awake()
    {
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _inputLocker = GetComponent<InputLock>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (!IsAttacking)
        {
            if (_lightAttack && !_onCooldownLight)
            {
                StartCoroutine(CooldownTimer(_lightCooldown, _lightAttack));
                DoLightAttack();

            }
            if (HeavyAttack && !_onCooldownHeavy)
            {
                StartCoroutine(CooldownTimer(_heavyCooldown, _lightAttack));
                _inputLocker.LockInput();
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

        GameObject hitBox = Instantiate(attackHitBox.Prefab, transform);
        hitBox.transform.position = hitBox.transform.position + direction * attackHitBox.Offset;

        if (isLight)
            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayLight));
        else
            StartCoroutine(DestroyAfterDelay(hitBox, _hitBoxDestroyDelayHeavy));
    }


    IEnumerator DestroyAfterDelay(GameObject hitBox, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(hitBox);
        if (_lightAttack)
        {
            StartCoroutine(DelayLightFinish());
        }
        else
        {
            _inputLocker.UnlockInput();
            HeavyAttack = false;
            IsAttacking = false;
        }
    }


    void DoLightAttack()
    {
        IsAttacking = true;
        StartCoroutine(SpawnHitBoxAfterDelay(_lightAttackHitBoxDelay, _lightAttack));
        RuntimeManager.PlayOneShot(MeleeLightSwing);
        _animator.SetTrigger("LightAttack");
    }

    void DoHeavyAttack()
    {
        IsAttacking = true;
        StartCoroutine(SpawnHitBoxAfterDelay(_heavyAttackHitBoxDelay, _lightAttack));
        _animator.SetTrigger("HeavyAttack");
        RuntimeManager.PlayOneShot("event:/Player/Attack_heavy");
    }

    IEnumerator SpawnHitBoxAfterDelay(float delay, bool isLight)
    {
        yield return new WaitForSeconds(delay);
        SpawnAttackHitBox(isLight);
    }


    IEnumerator DelayLightFinish()
    {
        yield return new WaitForSeconds(_delayLightFinish);
        _lightAttack = false;
        IsAttacking = false;
    }


    IEnumerator CooldownTimer(float cd, bool isLight)
    {
        if (isLight)
        {
            _onCooldownLight = true;
            yield return new WaitForSeconds(cd);
            _onCooldownLight = false;
        }
        else
        {
            _onCooldownHeavy = true;
            yield return new WaitForSeconds(cd);
            _onCooldownHeavy = false;
        }

    }

    #region Input System
    void OnLightAttack(InputValue input)
    {
        if (!_onCooldownLight && !_playerStats.HeavyMetal && !_playerStats.BowlingChampion)
            _lightAttack = true;
    }


    void OnHeavyAttack(InputValue input)
    {
        if (!_onCooldownHeavy && !_playerStats.BaseballConnoisseur && !_playerStats.BowlingChampion)
            HeavyAttack = true;
    }

    #endregion

    [Serializable]
    struct HitBox
    {
        public GameObject Prefab;
        public float Offset;
    }

}
