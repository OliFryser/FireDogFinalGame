using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private PersistentPlayerStats _peristentPlayerStats;
    public float MovementSpeed = 50.0f;
    public int MaxHealth = 6;
    public float Damage = 10.0f;
    public bool IsDead => _currentHealth <= 0;
    private int _currentHealth = 6;
    private HealthUIManager _healthUIManager;
    public float EnemyStunDuration;
    public float PlayerPushBack;
    public float EnemyPushBack;

    public float DodgeCooldown;

    public float DodgeDistance;

    public int MinimumCleaningReward = 2;
    public int MaximumCleaningReward = 2;
    public int CriticalAttackChance = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _healthUIManager = FindAnyObjectByType<HealthUIManager>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        _currentHealth = MaxHealth;
        _healthUIManager.UpdateHearts(_currentHealth);
        _peristentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
        _peristentPlayerStats.RegisterNewPlayerStats(this);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);
        _healthUIManager.UpdateHearts(_currentHealth);
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        MaxHealth += amount;
        _healthUIManager.CreateHearts();
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        _healthUIManager.UpdateHearts(_currentHealth);
    }

    public int CurrentHealth => _currentHealth;


    public void AddCoins(int amount)
    {
        _peristentPlayerStats.AddCoins(amount);
    }

    public void RemoveCoins(int amount)
    {
        _peristentPlayerStats.SpendCoins(amount);
    }

    public int Coins => _peristentPlayerStats.Coins;

    public bool PassiveHealing { get; internal set; }

    public void Reset()
    {
        MovementSpeed = 100.0f;
        MaxHealth = 6;
        Damage = 10.0f;
        _currentHealth = 6;
        EnemyStunDuration = 1.0f;
    }

    internal void IncreaseFlashLightRadius()
    {
        GetComponentInChildren<Light2D>().pointLightOuterRadius += .5f;
    }

    internal void IncreaseCriticalAttack()
    {
        CriticalAttackChance++;
    }

    internal void IncreaseCleaningReward()
    {
        MaximumCleaningReward++;
        MinimumCleaningReward = (MaximumCleaningReward / MinimumCleaningReward) + 1;
    }

    internal bool IsCritical()
        => UnityEngine.Random.Range(0f, 1f) * 100f < CriticalAttackChance;
}
