using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;
using Player;
using Unity.Mathematics;
using FMOD.Studio;

public class PlayerHitDetection : MonoBehaviour
{
    private static readonly int Damage = Animator.StringToHash("TakeDamage");

    private PlayerStats _playerStats;
    private CameraShake _cameraShake;
    private Movement _playerMovement;
    private Animator _animator;
    private Light2D _flashlight;
    private FlashEffect _flashEffect;
    private InvincibilityManager _invincibilityManager;
    private InputLock _inputLocker;

    [SerializeField]
    private GameObject _playerDeath;

    [SerializeField]
    private GameObject _deathScreen;

    public string hitSoundEventPath = "event:/Player/Damage";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerMovement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _flashlight = GetComponentInChildren<Light2D>(includeInactive: true);
        _flashEffect = GetComponent<FlashEffect>();
        _invincibilityManager = GetComponent<InvincibilityManager>();
        _inputLocker = GetComponent<InputLock>();
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

    private void KillPlayer()
    {
        // Ensure SnapshotManager is initialized before using it
        if (SnapshotManager.Instance == null)
        {
            SnapshotManager.Initialize();
        }

        SnapshotManager.Instance.ActivateDeathSnapshot();

        // Return player to hub.
        StartCoroutine(IgnoreCollision(3.8f));
        _playerStats.AddPlayerDeath();
        _playerStats.StartPlayerDeathSnapshot();
        _inputLocker.LockInput();
        Instantiate(_deathScreen, transform.position, quaternion.identity);
        _playerDeath.GetComponent<Animator>().SetTrigger("Death");
        StartCoroutine(PlayDeathAnimation(2.6f));
        RuntimeManager.PlayOneShot("event:/UI/In-GameUI/Death");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (_invincibilityManager == null || !_invincibilityManager.IsInvincible)
            {
                TakeDamage(1);
                // Apply pushback only if not invincible
                if (other.gameObject.GetComponent<EnemyMovement>() != null)
                {

                    Vector2 enemyDirection = other.gameObject.GetComponent<EnemyMovement>().GetEnemyDirection();

                    _playerMovement.GetPushed(enemyDirection);
                }
                else {

                    Vector2 enemyDirection = other.gameObject.transform.parent.GetComponent<EnemyMovement>().GetEnemyDirection();
                    _playerMovement.GetPushed(enemyDirection);              
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lamp Attack Ring"))
        {
            TakeDamage(1);
            Vector2 enemyDirection = (transform.position - other.gameObject.GetComponentInParent<LampMovement>().transform.position).normalized;
            _playerMovement.GetPushed(enemyDirection);
        }
    }

    private void TakeDamage(int damage)
    {
        if (_invincibilityManager == null || !_invincibilityManager.IsInvincible)
        {
            _playerStats.ApplyDamage(damage);
            if(_playerStats.IsDead) KillPlayer();
            RuntimeManager.PlayOneShot(hitSoundEventPath);
            _animator.SetTrigger(Damage);
            _flashEffect.CallDamageFlash();
            _cameraShake.StartShake();

            // Trigger invincibility after being hit
            StartCoroutine(_invincibilityManager.MakeInvincible());
        }
    }

    public IEnumerator MakeInvincible(float time)
    {
        if (_invincibilityManager == null) yield break;

        // Start invincibility in the manager
        StartCoroutine(_invincibilityManager.MakeInvincible(time));

        // Handle collision ignoring while invincible
        Physics2D.IgnoreLayerCollision(0, 3, true);
        Physics2D.IgnoreLayerCollision(0, 6, true);
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(0, 3, false);
        Physics2D.IgnoreLayerCollision(0, 6, false);
    }


    public IEnumerator IgnoreCollision(float time)
    {
        // Handle collision ignoring while invincible
        Physics2D.IgnoreLayerCollision(0, 3, true);
        Physics2D.IgnoreLayerCollision(0, 6, true);
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(0, 3, false);
        Physics2D.IgnoreLayerCollision(0, 6, false);
    }


    private IEnumerator PlayDeathAnimation(float time){
        yield return new WaitForSeconds(time);
        _playerDeath.GetComponent<SpriteRenderer>().enabled = false;
        _inputLocker.UnlockInput();
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }

}
