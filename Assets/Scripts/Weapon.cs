using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private Movement _playerMovement;

    public bool _heavyAttack = false;

    [SerializeField]
    private GameObject _lightHitBoxPrefab;

    [SerializeField]
    private float _hitBoxOffset = 0.75f;

    [SerializeField]
    private float _hitBoxDestroyDelay = 0.25f;
    private bool _lightAttack = false;

    void Start()
    {
        _playerMovement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (_lightAttack)
        {
            SpawnAttackHitBox(_lightHitBoxPrefab, _lightAttack);
            _lightAttack = false;
        }
        if (_heavyAttack)
        {
            _heavyAttack = false;
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
        StartCoroutine(DestroyAfterDelay(hitBox));
    }

    void OnLightAttack(InputValue _)
    {
        _lightAttack = true;
    }

    void OnHeavyAttack(InputValue _)
    {
        _heavyAttack = true;
    }

    IEnumerator DestroyAfterDelay(GameObject hitBox)
    {
        yield return new WaitForSeconds(_hitBoxDestroyDelay);
        Destroy(hitBox);
        _heavyAttack = false;
    }

    void OnEnemyHit()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }


}
