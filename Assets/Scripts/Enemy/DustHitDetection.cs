using UnityEngine;

public class DustHitDetection : EnemyHitDetection
{
    [SerializeField]
    private GameObject _particleDeath;
    protected override void Die()
    {
        Instantiate(_particleDeath, transform.position, Quaternion.identity);
        base.Die();
    }
}
