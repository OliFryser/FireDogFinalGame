using Unity.Mathematics;
using UnityEngine;

public class CouchHitDetection : EnemyHitDetection
{

    [SerializeField]
    private GameObject _dustEnemy;

    [SerializeField]
    private float offset = 2f;

    [SerializeField]
    private GameObject _couchCorpse;


    protected override void GetHitLightAttack()
    {
        base.GetHitLightAttack();
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
        Instantiate(_dustEnemy, transform.position, quaternion.identity);
    }

    protected override void Die(){
        base.Die();
        Instantiate(_couchCorpse, transform.position, quaternion.identity);
    }

}
