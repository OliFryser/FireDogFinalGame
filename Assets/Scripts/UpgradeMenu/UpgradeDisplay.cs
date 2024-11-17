using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [HideInInspector]
    public Upgrade Upgrade;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private TMP_Text _title;

    private UpgradeHandler _upgradeHandler;

    private void Start()
    {
        _upgradeHandler = FindAnyObjectByType<UpgradeHandler>();
        _title.text = Upgrade.Title;
        _button.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnUpgradeButtonClick()
    {
        _upgradeHandler.HandleUpgrade(Upgrade);
    }

    public void SetSelected()
    {
        _button.Select();
    }

    public void AddOnClickListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
}