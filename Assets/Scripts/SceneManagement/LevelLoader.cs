using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = FindAnyObjectByType<PlayerStats>();
    }

    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
            if (_playerStats.PassiveHealing)
                _playerStats.Heal(1);
        }
    }
}
