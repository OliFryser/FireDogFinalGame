using SceneManagement;

namespace Merchant
{
    public class HubMerchant : MerchantBase
    {
        private HubManager _hubManager;
        private HubUpgradeManager _hubUpgradeManager;
        private PersistentPlayerStats _persistentPlayerStats;

        protected override void Start()
        {
            base.Start();
            _hubManager = FindAnyObjectByType<HubManager>();
            _hubUpgradeManager = FindAnyObjectByType<HubUpgradeManager>();
            _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();

            if (_persistentPlayerStats.Deaths == 0)
                Destroy(gameObject);
        }

        public override void Interact()
        {
            if (!_hasInteracted)
            {
                _hasInteracted = true;
                _hubManager
                    .PlayMerchantDialogue(() => _hubUpgradeManager.ShowUpgradeMenu(_persistentPlayerStats.HubUpgrades));
            }
            else
                _hubUpgradeManager.ShowUpgradeMenu(_persistentPlayerStats.HubUpgrades);
        }
    }
}
