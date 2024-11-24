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
            case UpgradeType.DamageBoost:
                _playerStats.Damage *= 1.05f;
                break;
            case UpgradeType.SpeedBoost:
                _playerStats.MovementSpeed += 10;
                break;
            default:
                Debug.LogWarning("Undefined Upgrade in UpgradeHandler");
                break;
        }
    }
}
