using UnityEngine;
using FMODUnity;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    EventReference Enemyhit;
    private EnemyTracker _enemyTracker;

    private void Start()
    {
        _enemyTracker = FindFirstObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _health--;
            RuntimeManager.PlayOneShot(Enemyhit);
            if (_health == 0)
            {
                _enemyTracker.UnregisterEnemy();
                Destroy(gameObject);
            }
        }
    }
}
