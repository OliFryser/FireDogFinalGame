using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
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

    public int CoinAmount;

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

    public int GetCurrentHealth() => _currentHealth;


    public void AddCoins(int amount)
    {
        CoinAmount += amount;
    }

    public void RemoveCoins(int amount)
    {
        CoinAmount -= amount;
    }


    public void Reset()
    {
        MovementSpeed = 100.0f;
        MaxHealth = 6;
        Damage = 10.0f;
        _currentHealth = 6;
        EnemyStunDuration = 1.0f;
    }

}
