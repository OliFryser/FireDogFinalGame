using UnityEngine;
using UnityEngine.InputSystem;

public class PauseListener : MonoBehaviour
{
    private PauseMenu _pauseMenu;

    private void Awake()
    {
        _pauseMenu = FindAnyObjectByType<PauseMenu>(FindObjectsInactive.Include);
    }

    private void PauseGame()
    {
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        _pauseMenu.IsPaused = true;
    }

    private void ResumeGame()
    {
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        _pauseMenu.IsPaused = false;
    }

    void OnPause(InputValue _)
    {
        if (_pauseMenu.IsPaused)
            ResumeGame();
        else
            PauseGame();
    }
}
