using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    private UpgradeDisplay[] _displays;
    [SerializeField]
    private Upgrade[] _upgrades = new Upgrade[3];

    private void Start()
    {
        _displays = GetComponentsInChildren<UpgradeDisplay>(includeInactive: true);
        Debug.Log($"Displays found: {_displays.Length}");
        for (int i = 0; i < _displays.Length; i++)
        {
            _displays[i].Upgrade = _upgrades[i];
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _displays[0].SetSelected();
    }
}
