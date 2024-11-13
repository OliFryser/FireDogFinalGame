using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onUpgradeSelected;

    [SerializeField]
    private UpgradeDisplay[] _displays = new UpgradeDisplay[3];

    [SerializeField]
    private Upgrade[] _upgrades = new Upgrade[3];

    private void Awake()
    {
        for (int i = 0; i < _displays.Length; i++)
        {
            _displays[i].Upgrade = _upgrades[i];
            _displays[i].AddOnClickListener(OnUpgradeSelected);
        }
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectFirstControl();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        SelectFirstControl();
    }

    private void SelectFirstControl()
    {
        _displays[0].SetSelected();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnUpgradeSelected()
    {
        _onUpgradeSelected?.Invoke();
    }
}
