using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = FindAnyObjectByType<PlayerStats>();
    }

    private static void LoadNextScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerStats.PassiveHealing)
                _playerStats.Heal(1);
            _playerStats.CleaningSpreeDamageActive = false;
            _playerStats.AddToRoomCounter();
            LoadNextScene();
        }
    }
}
