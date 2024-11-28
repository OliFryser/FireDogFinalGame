using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HubUpgradeDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleTextField;

    [SerializeField]
    private TMP_Text _descriptionTextField;

    [SerializeField]
    private TMP_Text _tierTextField;

    [SerializeField]
    private TMP_Text _costTextField;

    [SerializeField]
    private Button _buyButton;

    private HubUpgrade _hubUpgrade;
    public HubUpgrade HubUpgrade => _hubUpgrade;

    private Action _refreshCallback;

    public void Populate(HubUpgrade hubUpgrade, Action refreshCallback)
    {
        _hubUpgrade = hubUpgrade;
        _refreshCallback = refreshCallback;
        _titleTextField.text = _hubUpgrade.HubUpgradeInfo.Title;
        _descriptionTextField.text = _hubUpgrade.HubUpgradeInfo.Description;
        _costTextField.text = _hubUpgrade.Cost.ToString();
        _tierTextField.text = $"Tier {_hubUpgrade.Tier} / {_hubUpgrade.HubUpgradeInfo.MaxTier}";
        SetupButton();

    }

    private void SetupButton()
    {
        if (_hubUpgrade.CanBuy)
        {
            _buyButton.onClick.RemoveAllListeners();
            _buyButton.interactable = true;
            _buyButton.onClick.AddListener(
                () =>
                {
                    _hubUpgrade.Upgrade();
                    _refreshCallback();
                });
        }
        else
            _buyButton.interactable = false;
    }

    public void Refresh()
    {
        _costTextField.text = _hubUpgrade.Cost.ToString();
        _tierTextField.text = $"Tier {_hubUpgrade.Tier} / {_hubUpgrade.HubUpgradeInfo.MaxTier}";

        SetupButton();
    }

    public void Select()
    {
        _buyButton.Select();
    }
}
