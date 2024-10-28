using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField]
    private float _movementSpeed = 2.0f;

    [SerializeField]
    private float _sightDistance = 5.0f;

    void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
    }

    void Update()
    {
        if (!PlayerIsInLineOfSight()) return;

        Vector2 direction = (_playerTransform.position - transform.position).normalized;

        transform.Translate(_movementSpeed * Time.deltaTime * direction);
    }

    bool PlayerIsInLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (_playerTransform.position - transform.position).normalized);
        if (!hit)
            return false;

        Debug.DrawRay(transform.position, (_playerTransform.position - transform.position).normalized);
        return hit.collider.CompareTag("Player") && hit.distance < _sightDistance;
    }
}
