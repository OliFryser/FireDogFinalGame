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
    private Animator _animator;
    private Light2D _flashlight;
    private bool _invincible;
    private FlashEffect _flashEffect;

    public string hitSoundEventPath = "event:/Player/Damage";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _flashlight = GetComponentInChildren<Light2D>(includeInactive: true);
        _flashEffect = GetComponent<FlashEffect>();

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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lamp Attack Ring"))
        {
            TakeDamage(1);
            Vector2 _enemyDirection = (transform.position - other.gameObject.GetComponentInParent<LampMovement>().transform.position).normalized;
            _playerMovement.GetPushed(_enemyDirection);
        }
    }

    private void TakeDamage(int damage)
    {
        if (!_invincible)
        {
            _playerStats.ApplyDamage(damage);

            RuntimeManager.PlayOneShot(hitSoundEventPath);
            _animator.SetTrigger("TakeDamage");
            _flashEffect.CallDamageFlash();

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
