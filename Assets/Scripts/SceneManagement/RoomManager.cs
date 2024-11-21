using System;
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
    private Movement _playerMovement;

    private void Start()
    {
        _inputLock = FindAnyObjectByType<InputLock>();
        _flashlight = FindAnyObjectByType<Flashlight>();
        _upgradeMenu = FindAnyObjectByType<UpgradeMenu>(FindObjectsInactive.Include);
        _merchantSpawner = FindAnyObjectByType<MerchantSpawner>();
        _door = FindAnyObjectByType<Door>();
        _musicController = GetComponentInChildren<MusicController>();
        _playerMovement = FindAnyObjectByType<Movement>();
    }

    public void ClearRoom()
    {
        StartCoroutine(CleanupSequence());
        _musicController.ClearRoom();
    }

    private IEnumerator CleanupSequence()
    {
        if (_screenFade != null)
        {
            if (_screenFade.TryGetComponent(out Movement playerMovement))
            {
                playerMovement.canMove = false;
            }
            _screenFade.FadeToBlack();
            if (!string.IsNullOrEmpty(_cleaningSoundEvent.Path))
            {
                FMODUnity.RuntimeManager.PlayOneShot(_cleaningSoundEvent);
            }
            yield return new WaitForSeconds(2f);
        }

        // Step 3: Disable dirty assets
        GameObject[] dirtyAssets = GameObject.FindGameObjectsWithTag("DirtyAsset");
        foreach (GameObject asset in dirtyAssets)
        {
            asset.SetActive(false);
        }

        // Wait for a bit (adjust duration as needed)
        yield return new WaitForSeconds(0.5f);

        // Step 4: Fix the room lighting
        FixLight();

        // Step 5: Spawn merchant if applicable
        SpawnMerchant();

        // Step 6: Fade back in
        if (_screenFade != null)
        {
            _screenFade.FadeFromBlack();
            yield return new WaitForSeconds(1f); // Wait for fade-in to finish

            // Re-enable player movement after the fade-in
            if (_screenFade.TryGetComponent(out Movement playerMovement))
            {
                playerMovement.canMove = true;
            }
        }
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
