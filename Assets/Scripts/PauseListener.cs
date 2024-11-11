using UnityEngine;
using UnityEngine.InputSystem;

public class PauseListener : MonoBehaviour
{
    private PauseManager _pauseManager;

    private void Awake()
    {
        _pauseManager = FindAnyObjectByType<PauseManager>();
    }

    void OnPause(InputValue _)
    {
        _pauseManager.OnPause();
    }
}
