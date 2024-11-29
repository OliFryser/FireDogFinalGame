using System.Collections;
using UnityEngine;
using FMODUnity;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _warmLight;

    [SerializeField]
    private GameObject _spookyLight;

    [SerializeField]
    private EventReference _cleaningSoundEvent;

    [SerializeField]
    private ScreenFade _screenFade;

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
        StartCoroutine(CleanupSequence());
        _musicController.ClearRoom();
    }

    private IEnumerator CleanupSequence()
    {
        _inputLock.LockInput();
        _screenFade.FadeToBlack();
        RuntimeManager.PlayOneShot(_cleaningSoundEvent);
        yield return new WaitForSeconds(0.7f);

        GameObject[] dirtyAssets = GameObject.FindGameObjectsWithTag("DirtyAsset");
        foreach (GameObject asset in dirtyAssets)
        {
            asset.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        FixLight();
        SpawnMerchant();

        _screenFade.FadeFromBlack();
        yield return new WaitForSeconds(0.5f);
        _inputLock.UnlockInput();

    }

    private void SpawnMerchant()
    {
        _merchantSpawner.SpawnMerchant();
    }

    private void FixLight()
    {
        _spookyLight.SetActive(false);
        _warmLight.SetActive(true);

        if (_flashlight != null)
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
