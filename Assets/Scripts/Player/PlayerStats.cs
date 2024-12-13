using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        private PersistentPlayerStats _persistentPlayerStats;
        public float MovementSpeed = 50.0f;
        public int MaxHealth = 6;
        public float DamageLight = 10.0f;
        public float DamageHeavy = 20.0f;
        public bool IsDead => _currentHealth <= 0;
        private int _currentHealth = 6;
        private HealthUIManager _healthUIManager;
        private Light2D _flashlight;
        public float EnemyStunDuration;
        public float PlayerPushBack;
        public float EnemyPushBack;

        public float DodgeCooldown;

        public float DodgeDistance;

        public bool DamageBoostUpgrade;

        public bool CleaningSpreeDamage;

        public bool CleaningSpreeDamageActive;

        public bool CleaningSpreeMoney;

        public float DamageBoostCounter;

        public int CleaningReward => _persistentPlayerStats.CleaningReward;
        public float CriticalAttackChance;

        public bool HeavyMetal;

        public bool BowlingChampion;

        public bool BaseballConnoisseur;

        public float EnemyPushBackSpeed;
        public int RoomNumber { get; private set; }
        public int Deaths => _persistentPlayerStats.Deaths;

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

        private void Start()
        {
            _flashlight = GetComponentInChildren<Light2D>();
            _currentHealth = MaxHealth;
            _healthUIManager.UpdateHearts(_currentHealth);
            _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
            CriticalAttackChance = _persistentPlayerStats.CriticalAttackChance;
            _flashlight.pointLightOuterRadius = _persistentPlayerStats.FlashlightRadius;
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
            if (_currentHealth + amount > MaxHealth)
                return;
            _currentHealth += amount;
            _healthUIManager.UpdateHearts(_currentHealth);
        }

        public int CurrentHealth => _currentHealth;

        public void AddCoins(int amount)
        {
            _persistentPlayerStats.AddCoins(amount);
        }

        public int Coins => _persistentPlayerStats.Coins;

        public bool PassiveHealing => _persistentPlayerStats.PassiveHealing;
        public bool FullHealth => _currentHealth == MaxHealth;
        public bool HasStartedLevel6 => _persistentPlayerStats.HasStartedLevel6;
        
        public void Reset()
        {
            MovementSpeed = 100.0f;
            MaxHealth = 6;
            DamageLight = 10.0f;
            DamageHeavy = 20.0f;
            _currentHealth = 6;
            EnemyStunDuration = 1.0f;
        }

        internal bool IsCritical()
            => UnityEngine.Random.Range(0f, 100f) < CriticalAttackChance;

        internal void AddPlayerDeath()
        {
            _persistentPlayerStats.AddPlayerDeath();
        }

        internal void AddToRoomCounter()
        {
            RoomNumber++;
        }

        public void StartLevel6()
        {
            _persistentPlayerStats.HasStartedLevel6 = true;
        }

        public void StartPlayerDeathSnapshot()
        {
            _persistentPlayerStats.StartPlayerDeathSnapshot();
        }
    }
}
