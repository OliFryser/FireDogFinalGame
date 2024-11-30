using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTracker : MonoBehaviour
{
    private int _enemyCount;

    [SerializeField]
    private UnityEvent _onRoomCleared;

    private bool _roomCleared;

    private void Start()
    {
        StartCoroutine(SlowEnemyCounter());
    }

    public void RegisterEnemy()
    {
        _enemyCount++;
    }

    public void UnregisterEnemy()
    {
        _enemyCount--;
        StartCoroutine(SpawnBuffer(_enemyCount));
    }

    // We add this buffer to handle if a couch dies before spawning it's dusts enemies
    private IEnumerator SpawnBuffer(int enemyCount)
    {
        if (_roomCleared)
            yield return null;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (enemyCount == 0)
        {
            StopAllCoroutines();
            _onRoomCleared?.Invoke();
            _roomCleared = true;
        }
    }

    // Safe guard if counter gets out of sync
    // Happens very rarely, and is hard to reproduce, so this is a hotfix
    // TODO Figure out actual problem and fix it
    private IEnumerator SlowEnemyCounter()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(5f);
            int enemyCount = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None).Length;
            StartCoroutine(SpawnBuffer(enemyCount));
        }
    }
}
