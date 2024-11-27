using Unity.Mathematics;
using UnityEngine;

public class LampHitDetection : EnemyHitDetection
{
    [SerializeField]
    private GameObject _lampCorpsePrefab;
    protected override void Die()
    {
        if (!_isDead)
            Instantiate(_lampCorpsePrefab, transform.position, quaternion.identity);
        base.Die();
    }
}