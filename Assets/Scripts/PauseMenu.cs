using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _audioPanel;
    private const string MAIN_MENU = "Main Menu";

    [HideInInspector]
    public bool IsPaused;

    [SerializeField]
    private Button _resumeButton;

    [SerializeField]
    private Slider _firstAudioControl;

    private void OnEnable()
    {
        SelectFirstControl();
    }

    private void SelectFirstControl()
    {
        if (_audioPanel.activeSelf)
            _firstAudioControl.Select();
        else
            EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectFirstControl();
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        Destroy(FindAnyObjectByType<PlayerStats>().gameObject);
        SceneManager.LoadScene(MAIN_MENU);
    }
    public void ToggleAudioPanel()
    {
        _audioPanel.SetActive(!_audioPanel.activeSelf);
        SelectFirstControl();
    }
}
