using UnityEngine;
using FMODUnity;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    EventReference Enemyhit;

    [SerializeField]
    private float _lightPushBack = 1;

    [SerializeField]
    private float _heavyPushBack = 2;

    private EnemyTracker _enemyTracker;

    private EnemyMovement _enemyMovement;

    private Movement _playerMovement;

    private void Start()
    {
        _enemyTracker = FindFirstObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
        _enemyMovement = GetComponent<EnemyMovement>();
        _playerMovement = FindFirstObjectByType<Movement>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _health--;
            _enemyMovement.GetPushedBack(_lightPushBack, false);
            RuntimeManager.PlayOneShot(Enemyhit);
        }

        else if (other.CompareTag("Heavy Weapon Hit Box")){
            _enemyMovement.GetPushedBack(_heavyPushBack, true);
            _health-=2;
        }

        else if (other.CompareTag("Player")){
            _playerMovement.GetPushed(_enemyMovement.GetEnemyDirection());
        }

        if (_health <= 0)
            {
                _enemyTracker.UnregisterEnemy();
                Destroy(gameObject);
            }
    }
}