using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private string MainMenu;
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;         // Freeze the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;         
        isPaused = false;
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;  // Reset time scale to normal
        SceneManager.LoadScene(MainMenu);  // Load the main menu scene
    }
    public void ToggleAudioPanel()
    {
        if (audioPanel != null)
        {
            audioPanel.SetActive(!audioPanel.activeSelf);
        }
        else
        {
            Debug.LogWarning("Audio Panel is not assigned in the Inspector.");
        }
    }
    public void CloseAudioPanel()
    {
        if (audioPanel != null)
        {
            audioPanel.SetActive(false); // Hide the Audio Panel
        }
    }
}
