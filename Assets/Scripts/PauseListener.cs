using UnityEngine;
using UnityEngine.InputSystem;

public class PauseListener : MonoBehaviour
{
    [SerializeField]
    private PauseMenu _pauseMenu;

    private bool _isPaused;

    private void PauseGame()
    {
        _pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        _isPaused = true;
    }

    private void ResumeGame()
    {
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    void OnPause(InputValue _)
    {
        if (_isPaused)
            ResumeGame();
        else
            PauseGame();
    }
}
