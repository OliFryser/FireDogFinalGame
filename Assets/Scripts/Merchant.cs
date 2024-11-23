using UnityEngine;
using UnityEngine.InputSystem;

public class Merchant : Interactable
{
    private ButtonPrompt _interactionPrompt;
    private PlayerInput _playerInput;
    private RoomManager _roomManager;

    protected override void Start()
    {
        base.Start();
        _playerInput = _playerTransform.GetComponent<PlayerInput>();
        _interactionPrompt = GetComponentInChildren<ButtonPrompt>();
        _roomManager = FindFirstObjectByType<RoomManager>();
    }

    private ButtonPromptTypes GetTypeFromCurrentControlScheme(string controlScheme)
    => controlScheme switch
    {
        "Keyboard&Mouse" => ButtonPromptTypes.Keyboard,
        "Gamepad" => ButtonPromptTypes.Playstation,
        _ => throw new System.NotImplementedException("Unsupported Control Device")
    };

    public override void Interact()
    {
        base.Interact();
        _roomManager.OpenUpgradeMenu();
    }

    public override void Highlight()
    {
        _interactionPrompt.ShowPrompt(GetTypeFromCurrentControlScheme(_playerInput.currentControlScheme));
    }

    public override void UnHighlight()
    {
        _interactionPrompt.HidePrompt();
    }
}
