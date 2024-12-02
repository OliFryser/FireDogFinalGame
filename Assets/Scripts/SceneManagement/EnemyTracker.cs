using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SceneManagement
{
    public class EnemyTracker : MonoBehaviour
    {
        public int EnemyCount { get; private set; }

        [SerializeField]
        private UnityEvent _onRoomCleared;

        private bool _roomCleared;

        private void Start()
        {
            StartCoroutine(SlowEnemyCounter());
        }

        public void RegisterEnemy()
        {
            EnemyCount++;
        }

        public void UnregisterEnemy()
        {
            EnemyCount--;
            StartCoroutine(SpawnBuffer());
        }

        // We add this buffer to handle if a couch dies before spawning it's dusts enemies
        private IEnumerator SpawnBuffer()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            if (_roomCleared)
                yield return null;
            if (EnemyCount != 0) yield break;
            StopAllCoroutines();
            _roomCleared = true;
            _onRoomCleared?.Invoke();
        }

        // Safeguard if counter gets out of sync
        // Happens very rarely, and is hard to reproduce, so this is a hotfix
        // TODO Figure out actual problem and fix it
        private IEnumerator SlowEnemyCounter()
        {
            for (; ; )
            {
                yield return new WaitForSeconds(5f);
                var enemyCount = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None).Length;
                StartCoroutine(SpawnBuffer(enemyCount));
            }
            // ReSharper disable once IteratorNeverReturns
        }
        
        private IEnumerator SpawnBuffer(int enemyCount)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            StopAllCoroutines();
            if (_roomCleared) yield break;
            if (enemyCount != 0) yield break;
            _roomCleared = true;
            _onRoomCleared?.Invoke();
        }
    }
}
