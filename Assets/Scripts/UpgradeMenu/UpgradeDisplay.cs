using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    [HideInInspector]
    public Upgrade Upgrade;

    private Button _button;

    [SerializeField]
    private TMP_Text _title;

    private void Start()
    {
        _button = GetComponentInChildren<Button>();
        _title.text = Upgrade.Title;
    }

    public void SetSelected()
    {
        _button.Select();
    }
}
