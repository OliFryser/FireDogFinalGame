using UnityEngine;
using FMODUnity;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private EventReference musicEvent;
    private FMOD.Studio.EventInstance musicInstance;

    void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.setParameterByName("BattleState", 1f);
        Debug.Log("BattleState set to 0");
        musicInstance.start();
    }
    public void SetBattleState(float value)
    {
        musicInstance.setParameterByName("BattleState", value);
    }
    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
    public void ClearRoom()
    {
        musicInstance.setParameterByName("BattleState", 0);
    }
}
