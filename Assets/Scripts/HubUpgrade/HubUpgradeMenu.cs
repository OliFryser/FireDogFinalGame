using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HubUpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _upgradeDisplayPrefab;

    [SerializeField]
    private GameObject _upgradeList;

    [SerializeField]
    private Button _backButton;

    private List<HubUpgradeDisplay> _displays = new();
    private InputLock _inputLock;

    private void Start()
    {
        _inputLock = FindAnyObjectByType<InputLock>();
        gameObject.SetActive(false);
    }

    internal void ShowUpgrades(List<HubUpgrade> hubUpgrades)
    {
        _inputLock.LockInput();
        gameObject.SetActive(true);

        foreach (var upgrade in hubUpgrades)
        {
            var display = Instantiate(_upgradeDisplayPrefab, _upgradeList.transform).GetComponent<HubUpgradeDisplay>();
            display.Populate(upgrade, OnUpgradeSelected);
            _displays.Add(display);
        }

        SelectFirstControl();
    }

    private void OnUpgradeSelected()
    {
        _displays.ForEach(d => d.Refresh());
    }

    private void SelectFirstControl()
    {
        var firstEnabledDisplay = _displays.FirstOrDefault(d => d.HubUpgrade.CanBuy);
        if (firstEnabledDisplay != null)
            firstEnabledDisplay.Select();
        else
            _backButton.Select();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectFirstControl();
        }
    }

    public void HideUpgrades()
    {
        _inputLock.UnlockInput();
        _displays.ForEach(d => Destroy(d.gameObject));
        _displays.Clear();
        gameObject.SetActive(false);
    }
}
