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

    public int Coins { get; private set; }
    public int Deaths { get; private set; }

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
        Coins += amount;
    }

    public void SpendCoins(int amount)
    {
        Coins -= amount;
    }

    internal void RegisterNewPlayerStats(PlayerStats playerStats)
    {
        _hubUpgradeHandler.SetPlayerStats(playerStats);
    }

    public void AddPlayerDeath()
    {
        Deaths++;
    }

    public void ResetPlayerProgress()
    {
        Destroy(gameObject);
    }
}
