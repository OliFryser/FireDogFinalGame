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
        if (_enemyCount == 0)
        {
            _onRoomCleared?.Invoke();
        }
    }
}
