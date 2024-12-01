using System.Collections.Generic;
using UnityEngine;

public class HubUpgradeManager : MonoBehaviour
{
    [SerializeField]
    private HubUpgradeMenu _hubUpgradeMenu;

    internal void ShowUpgradeMenu(List<HubUpgrade> hubUpgrades)
    {
        _hubUpgradeMenu.ShowUpgrades(hubUpgrades);
    }

    internal void HideUpgradeMenu()
    {
        _hubUpgradeMenu.HideUpgrades();
    }
}