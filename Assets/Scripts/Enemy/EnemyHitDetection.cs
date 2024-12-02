using UnityEngine;
using FMODUnity;
using SceneManagement;
using System.Collections;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    public float _health = 50.0f;

    [SerializeField]
    protected EventReference Enemyhit;

    [SerializeField]
    protected float _pushBackOnPlayerHit;

    protected EnemyTracker _enemyTracker;

    protected EnemyMovement _enemyMovement;

    protected PlayerStats _playerStats;

    protected CameraShake _cameraShake;

    protected bool _isDead;

    private FlashEffect _flashEffect;

    private float _damageboostTimer;

    protected bool _isInvincible;

    protected void Start()
    {
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
        _enemyMovement = GetComponent<EnemyMovement>();
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _cameraShake = FindAnyObjectByType<CameraShake>();
        _flashEffect = GetComponent<FlashEffect>();
    }

    protected virtual void Update(){
        if (_damageboostTimer > 0){
            _damageboostTimer -= Time.deltaTime;
        }
        else {
            _playerStats.DamageBoostCounter = 1f;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            if (!_isInvincible) {
            _flashEffect.CallDamageFlash();
            GetHitLightAttack();
            }
        }

        else if (other.CompareTag("Heavy Weapon Hit Box"))
        {   
            if (!_isInvincible) {
            _flashEffect.CallDamageFlash();
            GetHitHeavyAttack();
            }
        }

        else if (other.CompareTag("Dodge Roll Hit Box")){
            if (!_isInvincible) {
            _flashEffect.CallDamageFlash();
            GetHitDodgeRoll();
            }
        }

        if (_health <= 0)
        {
            Die();
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _enemyMovement.GetPushedBack(_pushBackOnPlayerHit, 0);
        }
        else if (_enemyMovement.IsPushedBack && _playerStats.BaseballConnoisseur){
            _enemyMovement.StopPush();
            GetHitCollision();
        }
    }

    protected virtual void GetHitLightAttack()
    {   
        _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack, _playerStats.EnemyStunDuration);
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
        float damage = _playerStats.DamageLight*_playerStats.DamageBoostCounter;
        if (_playerStats.IsCritical())
        {
            Debug.Log("Critical Hit");
            damage *= 2;
        }
        if (_playerStats.CleaningSpreeDamageActive){
            damage *= 1.25f;
        }
        _health -= damage;
        if (_playerStats.DamageBoostUpgrade && _playerStats.DamageBoostCounter < 1.3f){
            _playerStats.DamageBoostCounter+=0.06f;
            _damageboostTimer = 3f;
        }
        StartCoroutine(InvincibilityFrames(0.15f));
    }

    protected virtual void GetHitHeavyAttack()
    {
        _enemyMovement.GetPushedBack(_playerStats.EnemyPushBack * 2, _playerStats.EnemyStunDuration * 1.5f);
        float damage = _playerStats.DamageHeavy*_playerStats.DamageBoostCounter;
        if (_playerStats.IsCritical())
        {
            Debug.Log("Critical Hit");
            damage *= 2;
        }
        if (_playerStats.CleaningSpreeDamageActive){
            damage *= 1.25f;
        }
        _health -= damage;
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
        if (_playerStats.DamageBoostUpgrade && _playerStats.DamageBoostCounter < 1.3f){
            _playerStats.DamageBoostCounter+=0.06f;
            _damageboostTimer = 3f;
        }
        StartCoroutine(InvincibilityFrames(0.15f));
    }

    protected virtual void GetHitDodgeRoll(){
        float damage = _playerStats.DamageHeavy*_playerStats.DamageBoostCounter;
        if (_playerStats.IsCritical())
        {
            Debug.Log("Critical Hit");
            damage *= 2;
        }
        if (_playerStats.CleaningSpreeDamageActive){
            damage *= 1.25f;
        }
        _health -= damage;
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
        if (_playerStats.DamageBoostUpgrade && _playerStats.DamageBoostCounter < 1.3f){
            _playerStats.DamageBoostCounter+=0.06f;
            _damageboostTimer = 3f;
        }
        StartCoroutine(InvincibilityFrames(0.15f));
    }


    protected virtual void GetHitCollision(){
        RuntimeManager.PlayOneShot(Enemyhit);
        _cameraShake.StartShake();
        float damage = _playerStats.DamageLight*_playerStats.DamageBoostCounter;
        if (_playerStats.IsCritical())
        {
            Debug.Log("Critical Hit");
            damage *= 2;
        }
        if (_playerStats.CleaningSpreeDamageActive){
            damage *= 1.25f;
        }
        _health -= damage;
        if (_playerStats.DamageBoostUpgrade && _playerStats.DamageBoostCounter < 1.3f){
            _playerStats.DamageBoostCounter+=0.06f;
            _damageboostTimer = 3f;
        }
    }

    protected virtual void Die()
    {
        if (!_isDead)
        {
            _enemyTracker.UnregisterEnemy();
            _isDead = true;
            Destroy(gameObject);
        }
    }

    protected IEnumerator InvincibilityFrames(float time){
        _isInvincible = true;
        yield return new WaitForSeconds(time);
        _isInvincible = false;
    }

}
