using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _persistentPlayerStatsPrefab;
    
    private void Awake()
    {
        var player = FindAnyObjectByType<PlayerHitDetection>();
        if (player == null)
            SpawnPlayer();
        else
            player.transform.position = transform.position;

        var persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
        if (persistentPlayerStats != null) return;
        var instantiatedPersistentStats = Instantiate(_persistentPlayerStatsPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(instantiatedPersistentStats);
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(player);
    }
}
