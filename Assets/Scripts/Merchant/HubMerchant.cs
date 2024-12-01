public class HubMerchant : MerchantBase
{
    private HubUpgradeManager _hubUpgradeManager;
    private PersistentPlayerStats _persistentPlayerStats;

    protected override void Start()
    {
        base.Start();
        _hubUpgradeManager = FindAnyObjectByType<HubUpgradeManager>();
        _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
    }


    public override void Interact()
    {
        _hubUpgradeManager.ShowUpgradeMenu(_persistentPlayerStats.HubUpgrades);
    }
}
