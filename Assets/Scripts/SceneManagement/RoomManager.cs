using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private UpgradeMenu _upgradeMenu;

    private InputLock _inputLock;
    private void Start()
    {
        _inputLock = FindAnyObjectByType<InputLock>();
    }

    public void ClearRoom()
    {
        _upgradeMenu.Show();
        _inputLock.LockInput();
    }

    public void CloseUpgradeMenu()
    {
        _upgradeMenu.Hide();
        _inputLock.UnlockInput();
    }
}
