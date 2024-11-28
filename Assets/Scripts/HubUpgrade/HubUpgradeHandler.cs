using UnityEngine;

public class HubUpgradeHandler
{
    private PersistentPlayerStats _persistentPlayerStats;
    private PlayerStats _playerStats;

    public HubUpgradeHandler(PersistentPlayerStats persistentPlayerStats, PlayerStats playerStats)
    {
        _persistentPlayerStats = persistentPlayerStats;
        _playerStats = playerStats;
    }

    internal void HandleUpgrade(HubUpgradeInfo hubUpgradeInfo, int tier)
    {
        _persistentPlayerStats.SpendCoins(hubUpgradeInfo.BaseCost + hubUpgradeInfo.IncrementPerTier * tier);

        switch (hubUpgradeInfo.HubUpgradeType)
        {
            case HubUpgradeType.HealthGain:
                _playerStats.PassiveHealing = true;
                break;
            case HubUpgradeType.FlashLightRadius:
                _playerStats.IncreaseFlashLightRadius();
                break;
            case HubUpgradeType.CriticalAttack:
                _playerStats.IncreaseCriticalAttack();
                break;
            case HubUpgradeType.BetterCleaningLoot:
                _playerStats.IncreaseCleaningReward();
                break;
            default:
                Debug.LogWarning("Undefined Upgrade in UpgradeHandler");
                break;
        }
    }

    internal void SetPlayerStats(PlayerStats playerStats)
    {
        _playerStats = playerStats;
    }

    internal bool PlayerCanAffordUpgrade(int cost)
        => _persistentPlayerStats.Coins >= cost;
}