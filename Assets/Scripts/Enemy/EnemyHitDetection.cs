using UnityEngine;
using FMODUnity;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    private float _health = 50.0f;
    [SerializeField]
    EventReference Enemyhit;

    [SerializeField]
    private float _pushBackOnPlayerHit;

    private EnemyTracker _enemyTracker;

    private EnemyMovement _enemyMovement;

    private PlayerStats _playerStats;

    private CameraShake _cameraShake;

    private bool _isDead;

    private FlashEffect _flashEffect;

    private void Start()
    {
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
        _enemyMovement = GetComponent<EnemyMovement>();
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _cameraShake = FindAnyObjectByType<CameraShake>();
        _flashEffect = GetComponent<FlashEffect>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack, _playerStats.EnemyStunDuration);
            RuntimeManager.PlayOneShot(Enemyhit);
            _cameraShake.StartShake();
            _health -= _playerStats.Damage;
            _flashEffect.CallDamageFlash();
        }

        else if (other.CompareTag("Heavy Weapon Hit Box"))
        {
            _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack*2, _playerStats.EnemyStunDuration*2);
            _health -= _playerStats.Damage * 2;
            RuntimeManager.PlayOneShot(Enemyhit);
            _cameraShake.StartShake();
            _flashEffect.CallDamageFlash();
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _enemyMovement.GetPushedBack(_pushBackOnPlayerHit, 0);
        }
    }
}
