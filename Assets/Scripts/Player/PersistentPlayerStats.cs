using System.Collections.Generic;
using UnityEngine;

public class PersistentPlayerStats : MonoBehaviour
{
    private HubUpgradeHandler _hubUpgradeHandler;

    [SerializeField]
    private List<HubUpgradeInfo> _hubUpgradeInfos;

    [SerializeField]
    private List<HubUpgrade> _hubUpgrades;

    public List<HubUpgrade> HubUpgrades => _hubUpgrades;

    private int _coins = 50;
    public int Coins => _coins;

    private void Awake()
    {
        _hubUpgradeHandler = new HubUpgradeHandler(this, FindAnyObjectByType<PlayerStats>());
        foreach (var upgradeInfo in _hubUpgradeInfos)
        {
            _hubUpgrades.Add(new HubUpgrade(upgradeInfo, _hubUpgradeHandler));
        }
    }

    public void AddCoins(int amount)
    {
        _coins += amount;
    }

    public void SpendCoins(int amount)
    {
        _coins -= amount;
    }

    internal void RegisterNewPlayerStats(PlayerStats playerStats)
    {
        _hubUpgradeHandler.SetPlayerStats(playerStats);
    }
}
