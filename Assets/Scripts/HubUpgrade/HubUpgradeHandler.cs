using Player;
using UnityEngine;

public class HubUpgradeHandler
{
    private readonly PersistentPlayerStats _persistentPlayerStats;

    public HubUpgradeHandler(PersistentPlayerStats persistentPlayerStats)
    {
        _persistentPlayerStats = persistentPlayerStats;
    }

    internal void HandleUpgrade(HubUpgradeInfo hubUpgradeInfo, int tier)
    {
        _persistentPlayerStats.SpendCoins(hubUpgradeInfo.BaseCost + hubUpgradeInfo.IncrementPerTier * tier);

        switch (hubUpgradeInfo.HubUpgradeType)
        {
            case HubUpgradeType.HealthGain:
                _persistentPlayerStats.PassiveHealing = true;
                break;
            case HubUpgradeType.FlashLightRadius:
                _persistentPlayerStats.IncreaseFlashLightRadius();
                break;
            case HubUpgradeType.CriticalAttack:
                _persistentPlayerStats.IncreaseCriticalAttack();
                break;
            case HubUpgradeType.BetterCleaningLoot:
                _persistentPlayerStats.IncreaseCleaningReward();
                break;
            default:
                Debug.LogWarning("Undefined Upgrade in UpgradeHandler");
                break;
        }
    }

    internal bool PlayerCanAffordUpgrade(int cost)
        => _persistentPlayerStats.Coins >= cost;
}