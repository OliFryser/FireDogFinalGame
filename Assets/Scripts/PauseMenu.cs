using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _audioPanel;
    private const string MAIN_MENU = "Main Menu";

    [HideInInspector]
    public bool IsPaused;

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MAIN_MENU);
    }
    public void ToggleAudioPanel()
    {
        if (_audioPanel != null)
            _audioPanel.SetActive(!_audioPanel.activeSelf);
        else
            Debug.LogWarning("Audio Panel is not assigned in the Inspector.");
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void CloseAudioPanel()
    {
        if (_audioPanel != null)
            _audioPanel.SetActive(false);
    }
}
