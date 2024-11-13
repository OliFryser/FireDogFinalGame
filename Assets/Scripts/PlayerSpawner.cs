using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    private void Awake()
    {
        PlayerHitDetection player = FindAnyObjectByType<PlayerHitDetection>();
        if (player == null)
        {
            SpawnPlayer();
        }
        else
        {
            player.transform.position = transform.position;
            player.Initialize();
        }
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(player);
    }
}
