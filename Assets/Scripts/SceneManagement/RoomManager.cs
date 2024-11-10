using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private UpgradeMenu _upgradeMenu;

    [SerializeField]
    private GameObject _warmLight;

    [SerializeField]
    private GameObject _spookyLight;

    private InputLock _inputLock;

    private Flashlight _flashlight;

    private MusicController _musicController;

    private void Start()
    {
        _inputLock = FindAnyObjectByType<InputLock>();
        _flashlight = FindAnyObjectByType<Flashlight>();
        _musicController = Object.FindFirstObjectByType<MusicController>();
    }

    public void ClearRoom()
    {
        _musicController.ClearRoom();
        _upgradeMenu.Show();
        _inputLock.LockInput();
        _spookyLight.SetActive(false);
        _warmLight.SetActive(true);
        _flashlight.TurnOffFlashlight();
    }

    public void CloseUpgradeMenu()
    {
        _upgradeMenu.Hide();
        _inputLock.UnlockInput();
    }
}
