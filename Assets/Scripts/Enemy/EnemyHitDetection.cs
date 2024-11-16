using UnityEngine;
using FMODUnity;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    private float _health = 50.0f;
    [SerializeField]
    EventReference Enemyhit;

    [SerializeField]
    private float _lightPushBack = 2.0f;

    [SerializeField]
    private float _heavyPushBack = 3.0f;

    private EnemyTracker _enemyTracker;

    private EnemyMovement _enemyMovement;

    private PlayerStats _playerStats;

    private CameraShake _cameraShake;

    private bool _isDead;

    private void Start()
    {
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
        _enemyMovement = GetComponent<EnemyMovement>();
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _cameraShake = FindAnyObjectByType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _enemyMovement.GetPushedBack(_lightPushBack, false);
            RuntimeManager.PlayOneShot(Enemyhit);
            _cameraShake.StartShake();
            _health -= _playerStats.Damage;
        }

        else if (other.CompareTag("Heavy Weapon Hit Box"))
        {
            _enemyMovement.GetPushedBack(_heavyPushBack, true);
            _health -= _playerStats.Damage * 2;
            RuntimeManager.PlayOneShot(Enemyhit);
            _cameraShake.StartShake();
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
            _enemyMovement.GetPushedBack(_lightPushBack, false);
        }
    }
}
