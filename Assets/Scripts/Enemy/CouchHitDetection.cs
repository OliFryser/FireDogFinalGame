using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class CouchHitDetection : EnemyHitDetection
{
    [SerializeField]
    private GameObject _dustEnemy;

    [SerializeField]
    private GameObject _couchCorpseHorizontal;
    [SerializeField]
    private GameObject _couchCorpseUp;
    [SerializeField]
    private GameObject _couchCorpseDown;

    [SerializeField]
    private float offset = 2f;

    protected override void GetHitLightAttack()
    {
        base.GetHitLightAttack();
        if (_health < 40)
            SpawnDust();
    }

    private void SpawnDust()
    {
        var directionToPlayer = GetComponent<EnemyMovement>().GetDirectionToPlayer();

        var leftPosition = transform.position + Quaternion.Euler(0, 0, -45) * (directionToPlayer * offset);
        var rightPosition = transform.position + Quaternion.Euler(0, 0, 45) * (directionToPlayer * offset);

        Instantiate(_dustEnemy, leftPosition, quaternion.identity);
        Instantiate(_dustEnemy, rightPosition, quaternion.identity);
    }

    protected override void GetHitHeavyAttack()
    {
        base.GetHitHeavyAttack();
        if (_health < 40)
            SpawnDust();
    }

    protected override void Die()
    {
        Vector3 direction = Utils.GetCardinalDirection(_enemyMovement.GetEnemyDirection());
        GameObject prefab = GetCorpsePrefabFromDirection(direction);
        Instantiate(prefab, transform.position, quaternion.identity);
        base.Die();
    }

    private GameObject GetCorpsePrefabFromDirection(Vector3 direction)
    {
        if (direction == Vector3.left || direction == Vector3.right)
            return _couchCorpseHorizontal;
        else if (direction == Vector3.up)
            return _couchCorpseUp;
        else
            return _couchCorpseDown;
    }


}
