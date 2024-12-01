using System;
using UnityEngine;

[Serializable]
public class HubUpgrade
{
    [SerializeField]
    private HubUpgradeInfo _hubUpgradeInfo;
    public HubUpgradeInfo HubUpgradeInfo => _hubUpgradeInfo;
    private HubUpgradeHandler _hubUpgradeHandler;
    private int _tier = 0;
    public int Cost => _hubUpgradeInfo.BaseCost + _hubUpgradeInfo.IncrementPerTier * _tier;
    public int Tier => _tier;
    private bool IsMaxTier => _tier >= _hubUpgradeInfo.MaxTier;
    private bool CanAfford => _hubUpgradeHandler.PlayerCanAffordUpgrade(Cost);
    public bool CanBuy => !IsMaxTier && CanAfford;


    public HubUpgrade(HubUpgradeInfo hubUpgradeInfo, HubUpgradeHandler hubUpgradeHandler)
    {
        _hubUpgradeInfo = hubUpgradeInfo;
        _hubUpgradeHandler = hubUpgradeHandler;
    }

    public void Upgrade()
    {
        if (IsMaxTier) return;
        _hubUpgradeHandler.HandleUpgrade(_hubUpgradeInfo, _tier);
        _tier++;
    }


}