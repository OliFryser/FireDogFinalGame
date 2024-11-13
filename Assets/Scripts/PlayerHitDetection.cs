using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetection : MonoBehaviour
{

    private PlayerStats _playerStats;
    private CameraShake _cameraShake;
    private Movement _playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _cameraShake = FindAnyObjectByType<CameraShake>();
        _playerMovement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStats.CurrentHealth <= 0) {
            //Return player to hub.
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene((currentScene%currentScene)+1);
            _playerStats.Reset();
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {   
            _playerStats.CurrentHealth--;
            Vector2 _enemyDirection = other.gameObject.GetComponent<EnemyMovement>().GetEnemyDirection();
            //RuntimeManager.PlayOneShot(Enemyhit);
            _playerMovement.GetPushed(_enemyDirection);
            _cameraShake.StartShake();

        }
    }
}
