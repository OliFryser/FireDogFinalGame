using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _persistentPlayerStatsPrefab;
    private void Awake()
    {
        PlayerHitDetection player = FindAnyObjectByType<PlayerHitDetection>();
        if (player == null)
            SpawnPlayer();
        else
            player.transform.position = transform.position;

        PersistentPlayerStats persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
        if (persistentPlayerStats == null)
        {
            var instantiatedPersistentStats = Instantiate(_persistentPlayerStatsPrefab, transform.position, Quaternion.identity);
            DontDestroyOnLoad(instantiatedPersistentStats);
        }
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(player);
    }
}
