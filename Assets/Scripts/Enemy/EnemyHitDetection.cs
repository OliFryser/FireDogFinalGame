using UnityEngine;
using FMODUnity;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    protected float _health = 50.0f;
    [SerializeField]
    protected EventReference Enemyhit;

    [SerializeField]
    protected float _pushBackOnPlayerHit;

    protected EnemyTracker _enemyTracker;

    protected EnemyMovement _enemyMovement;

    protected PlayerStats _playerStats;

    protected CameraShake _cameraShake;

    protected bool _isDead;

    protected void Start()
    {
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
        _enemyMovement = GetComponent<EnemyMovement>();
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _cameraShake = FindAnyObjectByType<CameraShake>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            GetHitLightAttack();
        }

        else if (other.CompareTag("Heavy Weapon Hit Box"))
        {
           GetHitHeavyAttack();
        }

        if (_health <= 0)
        {
            Die();
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _enemyMovement.GetPushedBack(_pushBackOnPlayerHit, 0);
        }
    }

    protected virtual void GetHitLightAttack(){
        _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack, _playerStats.EnemyStunDuration);
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
        _health -= _playerStats.Damage;
    }

    protected virtual void GetHitHeavyAttack(){
        _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack*2, _playerStats.EnemyStunDuration*2);
        _health -= _playerStats.Damage * 2;
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
    }

    protected virtual void Die () {
        if (!_isDead)
        {
            _enemyTracker.UnregisterEnemy();
            Destroy(gameObject);
            _isDead = true;
        }
    }

}
