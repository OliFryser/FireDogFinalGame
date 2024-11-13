using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _warmLight;

    [SerializeField]
    private GameObject _spookyLight;

    private UpgradeMenu _upgradeMenu;
    private InputLock _inputLock;
    private Flashlight _flashlight;
    private MusicController _musicController;
    private MerchantSpawner _merchantSpawner;
    private Door _door;

    private void Start()
    {
        _inputLock = FindAnyObjectByType<InputLock>();
        _flashlight = FindAnyObjectByType<Flashlight>();
        _upgradeMenu = FindAnyObjectByType<UpgradeMenu>(FindObjectsInactive.Include);
        _merchantSpawner = FindAnyObjectByType<MerchantSpawner>();
        _door = FindAnyObjectByType<Door>();
        _musicController = GetComponentInChildren<MusicController>();
    }

    public void ClearRoom()
    {
        _musicController.ClearRoom();
        FixLight();
        SpawnMerchant();
    }

    private void SpawnMerchant()
    {
        _merchantSpawner.SpawnMerchant();
    }

    private void FixLight()
    {
        _spookyLight.SetActive(false);
        _warmLight.SetActive(true);
        _flashlight.TurnOffFlashlight();
    }

    public void CloseUpgradeMenu()
    {
        _upgradeMenu.Hide();
        _inputLock.UnlockInput();
        _door.OpenDoor();
    }

    public void OpenUpgradeMenu()
    {
        _upgradeMenu.Show();
        _inputLock.LockInput();
    }
}
