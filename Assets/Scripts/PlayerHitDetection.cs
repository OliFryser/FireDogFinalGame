using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerHitDetection : MonoBehaviour
{

    private PlayerStats _playerStats;
    private CameraShake _cameraShake;
    private Movement _playerMovement;
    private bool _inCollision;
    private float _timeCounter = 0;
    private Animator _animator;
    private Light2D _flashlight;
    private HealthUIManager _healthUIManager;

    public string hitSoundEventPath = "event:/Player/Damage";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _flashlight = GetComponentInChildren<Light2D>(includeInactive: true);
        Initialize();
    }

    public void Initialize()
    {
        _cameraShake = FindAnyObjectByType<CameraShake>();
        _healthUIManager = FindAnyObjectByType<HealthUIManager>();
        _flashlight.gameObject.SetActive(true);
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
            _healthUIManager.UpdateHearts();
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
        _playerStats.CurrentHealth = Mathf.Clamp(_playerStats.CurrentHealth, 0, _playerStats.MaxHealth);

        _healthUIManager?.UpdateHearts();

        RuntimeManager.PlayOneShot(hitSoundEventPath);
        _animator.SetTrigger("TakeDamage");

        _cameraShake.StartShake();
    }
}
