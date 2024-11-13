using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetection : MonoBehaviour
{

    private PlayerStats _playerStats;
    private CameraShake _cameraShake;
    private Movement _playerMovement;
    private bool _inCollision;
    private float _timeCounter = 0;
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _cameraShake = FindAnyObjectByType<CameraShake>();
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inCollision)
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= 0.8)
            {
                TakeDamage(1);
                _timeCounter = 0;
            }
        }
        else
            _timeCounter = 0;

        if (_playerStats.CurrentHealth <= 0)
        {
            //Return player to hub.
            _playerStats.Reset();
            SceneManager.LoadScene(1);
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Vector2 _enemyDirection = other.gameObject.GetComponent<EnemyMovement>().GetEnemyDirection();
            _playerMovement.GetPushed(_enemyDirection);
            _inCollision = true;
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _inCollision = false;
        }
    }


    private void TakeDamage(int damage)
    {
        _playerStats.CurrentHealth -= damage;

        _animator.SetTrigger("TakeDamage");

        _cameraShake.StartShake();
    }
}
