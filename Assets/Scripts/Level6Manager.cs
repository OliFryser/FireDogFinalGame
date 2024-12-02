using UnityEngine;
using System.Collections.Generic;
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

    [FormerlySerializedAs("cleanupManager")]
    [Header("Cleanup Manager")]
    [SerializeField]
    private CleanupManager _cleanupManager;

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
    
    public void OnPlayerDeath()
    {
        if (_musicController != null)
        {
            _musicController.ResetBattleState();
        }
    }

    private void HandleLampDeath()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            OpenDoorToNextLevel();
        }
    }

    private void OpenDoorToNextLevel()
    {
        if (_doorToNextLevel != null)
        {
            _doorToNextLevel.OpenDoor();
        }

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
