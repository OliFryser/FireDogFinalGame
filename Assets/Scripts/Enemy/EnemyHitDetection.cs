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

    private FlashEffect _flashEffect;

    protected void Start()
    {
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
        _enemyMovement = GetComponent<EnemyMovement>();
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _cameraShake = FindAnyObjectByType<CameraShake>();
        _flashEffect = GetComponent<FlashEffect>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _flashEffect.CallDamageFlash();
            GetHitLightAttack();
        }

        else if (other.CompareTag("Heavy Weapon Hit Box"))
        {
            _flashEffect.CallDamageFlash();
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

    protected virtual void GetHitLightAttack()
    {
        _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack, _playerStats.EnemyStunDuration);
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
        _health -= _playerStats.Damage;
    }

    protected virtual void GetHitHeavyAttack()
    {
        _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack * 2, _playerStats.EnemyStunDuration * 2);
        _health -= _playerStats.Damage * 2;
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
    }

    protected virtual void Die()
    {
        if (!_isDead)
        {
            _enemyTracker.UnregisterEnemy();
            _isDead = true;
            Destroy(gameObject);
        }
    }

}
