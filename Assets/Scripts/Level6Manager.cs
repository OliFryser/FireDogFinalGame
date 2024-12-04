using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class Level6 : MonoBehaviour
{
    [FormerlySerializedAs("musicController")]
    [Header("Music Controller")]
    [SerializeField]
    private MusicController _musicController;

    [FormerlySerializedAs("roomManager")]
    [Header("Room Manager")]
    [SerializeField]
    private RoomManager _roomManager;

    [FormerlySerializedAs("doorToNextLevel")]
    [Header("Door to Next Level")]
    [SerializeField]
    private Door _doorToNextLevel;
    
    private int remainingEnemies;

    private void Awake()
    {
        if (_musicController != null)
        {
            _musicController.SetBattleState(0.5f);
        }
    }

    private void Start()
    {
        FindAnyObjectByType<Flashlight>().TurnOffFlashlight();
    }
    
    public void OnPlayerDeath()
    {
        if (_musicController != null)
        {
            _musicController.ResetBattleState();
        }
    }

    public void ClearRoom()
    {
        if (_musicController != null)
        {
            _musicController.ClearRoom();
        }

        if (_roomManager != null)
        {
            _roomManager.ClearRoom();
        }
    }

    private void OnDestroy()
    {
        if (_musicController != null)
        {
            _musicController.ResetBattleState();
        }
    }
}
