using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _audioPanel;

    [SerializeField]
    private Selectable _playButton;

    [SerializeField]
    private Selectable _firstAudioSelectable;

    [SerializeField] private EventReference mainMenuMusicEvent;
    private FMOD.Studio.EventInstance musicInstance;

    void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(mainMenuMusicEvent);
        musicInstance.start();
    }

    private void SelectFirstSelectable()
    {
        if (_audioPanel.activeSelf)
            _firstAudioSelectable.Select();
        else
            _playButton.Select();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            SelectFirstSelectable();
    }

    public void StartGame()
    {
        StopMusic();

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void ToggleAudioPanel()
    {
        if (_audioPanel != null)
        {
            _audioPanel.SetActive(!_audioPanel.activeSelf);
            SelectFirstSelectable();
        }
        else
        {
            Debug.LogWarning("Audio Panel is not assigned in the Inspector.");
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

