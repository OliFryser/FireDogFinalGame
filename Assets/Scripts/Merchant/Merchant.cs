using Dialogue;
using UnityEngine;

public class Merchant : MerchantBase
{
    private RoomManager _roomManager;
    private DialogueManager _dialogueManager;
    private PlayerStats _playerStats;

    protected override void Start()
    {
        base.Start();
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _dialogueManager = FindAnyObjectByType<DialogueManager>();
        _roomManager = FindFirstObjectByType<RoomManager>();
    }


    public override void Interact()
    {
        base.Interact();
        _dialogueManager
            .PlayRoomDialogue(
                _playerStats.Deaths, 
                _playerStats.RoomNumber, 
                _roomManager.OpenUpgradeMenu);
    }
}
