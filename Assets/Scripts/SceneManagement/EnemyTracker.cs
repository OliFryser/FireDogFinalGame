using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTracker : MonoBehaviour
{
    private int _enemyCount;

    [SerializeField]
    private UnityEvent _onRoomCleared;

    public void RegisterEnemy()
    {
        _enemyCount++;
    }

    public void UnregisterEnemy()
    {
        _enemyCount--;
        StartCoroutine(SpawnBuffer());
    }
    // We add this buffer to handle if a couch dies before spawning it's dusts enemies
    private IEnumerator SpawnBuffer()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (_enemyCount == 0)
        {
            StopAllCoroutines();
            _onRoomCleared?.Invoke();
        }
    }

    public int EnemiesLeft()
    {
        return _enemyCount;
    }
}
