using FMOD;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] _options = new Button[3];

    public void Show()
    {
        gameObject.SetActive(true);
        _options[0].Select();
    }
}
