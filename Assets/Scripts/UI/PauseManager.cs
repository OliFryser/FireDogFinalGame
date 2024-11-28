using FMODUnity;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private PauseMenu _pauseMenu;
    private MusicController _musicController;
    private InputLock _inputLock;

    private void Awake()
    {
        _pauseMenu = FindAnyObjectByType<PauseMenu>(FindObjectsInactive.Include);
        _musicController = FindAnyObjectByType<MusicController>();
    }

    private void Start()
    {
        _inputLock = FindAnyObjectByType<InputLock>();
    }

    private void PauseGame()
    {
        _inputLock.LockInput();
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        _pauseMenu.IsPaused = true;
        if (_musicController != null) _musicController.SetPauseVolume(true);
        RuntimeManager.PlayOneShot("event:/UI/GUI/Back");
    }

    public void ResumeGame()
    {
        _inputLock.UnlockInput();
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        _pauseMenu.IsPaused = false;
        if (_musicController != null) _musicController.SetPauseVolume(false);
    }

    internal void OnPause()
    {
        if (_pauseMenu.IsPaused)
            ResumeGame();
        else
            PauseGame();
    }
}
