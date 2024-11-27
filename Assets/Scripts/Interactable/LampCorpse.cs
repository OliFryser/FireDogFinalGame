using System.Collections;
using UnityEngine;
public class LampCorpse : Cleanable
{
    private EnemyTracker _enemyTracker;
    protected override void Start()
    {
        base.Start();
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
    }

    public override void Interact()
    {
        base.Interact();
        _enemyTracker.UnregisterEnemy();
        StartCoroutine(DestroyCorpse(0.9f));
    }

    private IEnumerator DestroyCorpse(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}