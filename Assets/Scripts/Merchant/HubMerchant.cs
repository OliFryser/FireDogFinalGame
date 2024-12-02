namespace Merchant
{
    public class HubMerchant : MerchantBase
    {
        private HubUpgradeManager _hubUpgradeManager;
        private PersistentPlayerStats _persistentPlayerStats;

        protected override void Start()
        {
            base.Start();
            //_hubManager = FindAnyObjectByType<HubManager>();
            _hubUpgradeManager = FindAnyObjectByType<HubUpgradeManager>();
            _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();

            if (_persistentPlayerStats.Deaths == 0)
                Destroy(gameObject);
        }

        public override void Interact()
        {
            _hubUpgradeManager.ShowUpgradeMenu(_persistentPlayerStats.HubUpgrades);
        }
    }
}
