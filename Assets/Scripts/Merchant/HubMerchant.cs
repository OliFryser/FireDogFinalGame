using System.IO.Compression;
using SceneManagement;

public class HubMerchant : MerchantBase
{
    private HubUpgradeManager _hubUpgradeManager;
    private PersistentPlayerStats _persistentPlayerStats;
    private HubManager _hubManager;

    protected override void Start()
    {
        base.Start();
        _hubManager = FindAnyObjectByType<HubManager>();
        _hubUpgradeManager = FindAnyObjectByType<HubUpgradeManager>();
        _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
        
        if(_persistentPlayerStats.Deaths == 0)
            Destroy(gameObject);
    }


    public override void Interact()
    {
        _hubManager
            .PlayMerchantDialogue(() => _hubUpgradeManager.ShowUpgradeMenu(_persistentPlayerStats.HubUpgrades));
    }
}
