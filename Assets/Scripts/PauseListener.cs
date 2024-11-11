using UnityEngine;
using UnityEngine.InputSystem;

public class PauseListener : MonoBehaviour
{
    private PauseMenu _pauseMenu;
    private MusicController _musicController;

    private void Awake()
    {
        _pauseMenu = FindAnyObjectByType<PauseMenu>(FindObjectsInactive.Include);
        _musicController = FindAnyObjectByType<MusicController>();
    }

    private void PauseGame()
    {
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        _pauseMenu.IsPaused = true;
        if (_musicController != null) _musicController.SetPauseVolume(true);
    }

    private void ResumeGame()
    {
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        _pauseMenu.IsPaused = false;
        if (_musicController != null) _musicController.SetPauseVolume(false);
    }

    void OnPause(InputValue _)
    {
        if (_pauseMenu.IsPaused)
            ResumeGame();
        else
            PauseGame();
    }
}
