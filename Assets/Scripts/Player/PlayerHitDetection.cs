using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHitDetection : MonoBehaviour
{

    private PlayerStats _playerStats;
    private CameraShake _cameraShake;
    private Movement _playerMovement;
    private bool _inCollision;
    private float _timeCounter = 0;
    private Animator _animator;
    private Light2D _flashlight;
    private bool _invincible;

    public string hitSoundEventPath = "event:/Player/Damage";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _flashlight = GetComponentInChildren<Light2D>(includeInactive: true);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _cameraShake = FindAnyObjectByType<CameraShake>();
        _flashlight.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

        if (_playerStats.IsDead)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        //Return player to hub.
        Destroy(gameObject);
        SceneManager.LoadScene(2);
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
        if (!_invincible)
        {
            _playerStats.ApplyDamage(damage);

            RuntimeManager.PlayOneShot(hitSoundEventPath);
            _animator.SetTrigger("TakeDamage");

            _cameraShake.StartShake();
        }
    }

    public IEnumerator MakeInvincible(float time)
    {
        _invincible = true;
        Physics2D.IgnoreLayerCollision(0, 2, true);
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(0, 2, false);
        _invincible = false;
    }
}
