using Player;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = FindAnyObjectByType<PlayerStats>();
    }
    public void HandleUpgrade(Upgrade upgrade)
    {
        switch (upgrade.UpgradeType)
        {
            case UpgradeType.HealthBoost:
                _playerStats.IncreaseHealth(2);
                break;
            case UpgradeType.DamageLightBoost:
                _playerStats.DamageLight += 2.5f;
                break;
            case UpgradeType.DamageHeavyBoost:
                _playerStats.DamageHeavy += 5f;
                break;
            case UpgradeType.SpeedBoost:
                _playerStats.MovementSpeed += 10;
                break;
            case UpgradeType.StackingDamageBoost:
                _playerStats.DamageBoostUpgrade = true;
                break;
            case UpgradeType.CritChanceBoost:
                _playerStats.CriticalAttackChance += 5;
                break;
            case UpgradeType.LargeCritBoost:
                _playerStats.CriticalAttackChance += 20;
                _playerStats.DamageLight -= 2.5f;
                _playerStats.DamageHeavy -= 5f;
                break;
            case UpgradeType.CleaningSpreeDamage:
                _playerStats.CleaningSpreeDamage = true;
                break;
            case UpgradeType.CleaningSpreeMoney:
                _playerStats.CleaningSpreeMoney = true;
                break;
            case UpgradeType.HeavyMetal:
                _playerStats.HeavyMetal = true;
                _playerStats.BaseballConnoisseur = false;
                _playerStats.BowlingChampion = false;
                _playerStats.DamageHeavy *= 3;
                _playerStats.EnemyPushBack *= 2;
                _playerStats.EnemyStunDuration *= 3;
                break;
            case UpgradeType.BowlingChampion:
                _playerStats.BowlingChampion = true;
                _playerStats.HeavyMetal = false;
                _playerStats.BaseballConnoisseur = false;
                _playerStats.DodgeCooldown = 0.3f;
                _playerStats.DodgeDistance += 40;
                break;
            case UpgradeType.BaseballConnoisseur:
                _playerStats.BaseballConnoisseur = true;
                _playerStats.HeavyMetal = false;
                _playerStats.BowlingChampion = false;
                _playerStats.EnemyPushBack *= 8;
                _playerStats.EnemyPushBackSpeed *=2;
                _playerStats.EnemyStunDuration *=2;
                break;
            default:
                Debug.LogWarning("Undefined Upgrade in UpgradeHandler");
                break;
        }
    }
}
