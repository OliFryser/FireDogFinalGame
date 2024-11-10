using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject audioPanel;

    [SerializeField] private EventReference mainMenuMusicEvent;
    private FMOD.Studio.EventInstance musicInstance;

    void Start()
    {
        // Create and start playing the music event
        if (!mainMenuMusicEvent.IsNull)
        {
            musicInstance = RuntimeManager.CreateInstance(mainMenuMusicEvent);
            musicInstance.start();
        }
        else
        {
            Debug.LogWarning("Main Menu Music Event is not assigned in the Inspector.");
        }
    }

    public void StartGame()
    {
        StopMusic();

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
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
    public void QuitGame()
    {
        Debug.Log("Game is quitting..."); // This line will show in the Console while testing in the Editor
        Application.Quit();
    }
    void OnDestroy()
    {
        // Ensure the music instance is stopped and released when the object is destroyed
        StopMusic();
    }

    private void StopMusic()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
            musicInstance.clearHandle();
        }
    }
}

