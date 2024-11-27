using System;
using UnityEngine;
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

    public int CleaningReward;

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

    public void Reset()
    {
        MovementSpeed = 100.0f;
        MaxHealth = 6;
        Damage = 10.0f;
        _currentHealth = 6;
        EnemyStunDuration = 1.0f;
    }

}
