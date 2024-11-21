using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class SpawnDustEnemy : EnemyHitDetection
{

    [SerializeField]
    private GameObject _dustEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack, _playerStats.EnemyStunDuration);
            RuntimeManager.PlayOneShot(Enemyhit);
            _cameraShake.StartShake();
            _health -= _playerStats.Damage;
            Instantiate(_dustEnemy, transform);
        }

        else if (other.CompareTag("Heavy Weapon Hit Box"))
        {
             _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack*2, _playerStats.EnemyStunDuration*2);
            _health -= _playerStats.Damage * 2;
            RuntimeManager.PlayOneShot(Enemyhit);
            _cameraShake.StartShake();
            Instantiate(_dustEnemy, transform);
        }

        if (_health <= 0)
        {
            if (!_isDead) 
            {
                _enemyTracker.UnregisterEnemy();
                Destroy(gameObject);
                _isDead = true;
            }
        }
    }
}
