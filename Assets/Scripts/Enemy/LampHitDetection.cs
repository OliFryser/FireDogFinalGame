using Unity.Mathematics;
using UnityEngine;
using FMODUnity;

public class LampHitDetection : EnemyHitDetection
{
    [SerializeField]
    private GameObject _lampCorpsePrefab;

    protected override void Die()
    {
        if (!_isDead)
            Instantiate(_lampCorpsePrefab, transform.position, quaternion.identity);
        base.Die();
        RuntimeManager.PlayOneShotAttached("event:/Enemy/Lamp_death", this.gameObject);
    }

    public void WallCollisionDetected(Collision2D collision){
        if (_enemyMovement.IsPushedBack && _playerStats.BaseballConnoisseur){
                GetHitCollision();
        }
    }

}