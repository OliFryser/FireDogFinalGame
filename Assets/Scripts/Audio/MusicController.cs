using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private EventReference musicEvent;
    private FMOD.Studio.EventInstance musicInstance;

    [Header("Battle State Parameters")]
    [Tooltip("Initial value for the BattleState parameter. Default is 0.5.")]
    [SerializeField]
    private float initialBattleState = 0.5f;

    [Tooltip("BattleState value to set when clearing the room. Default is 0.")]
    [SerializeField]
    private float clearRoomState = 0.0f;

    void Start()
    {
        // Initialize and start the music event
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.setParameterByName("BattleState", initialBattleState);
        musicInstance.start();
    }

    /// <summary>
    /// Sets the BattleState parameter.
    /// </summary>
    public void SetBattleState(float value)
    {
        musicInstance.setParameterByName("BattleState", value);
    }

    /// <summary>
    /// Resets the BattleState to its initial value.
    /// </summary>
    public void ResetBattleState()
    {
        SetBattleState(initialBattleState);
    }

    /// <summary>
    /// Clears the room by setting the BattleState to clearRoomState.
    /// </summary>
    public void ClearRoom()
    {
        SetBattleState(clearRoomState);
    }

    /// <summary>
    /// Sets the pause volume based on the game's pause state.
    /// </summary>
    public void SetPauseVolume(bool isPaused)
    {
        musicInstance.setParameterByName("PauseVolume", isPaused ? 1f : 0f);
    }

    void OnDestroy()
    {
        // Clean up music instance
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }
}
