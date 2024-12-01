using UnityEngine.InputSystem;

public abstract class MerchantBase : Interactable
{
    private ButtonPrompt _interactionPrompt;
    private PlayerInput _playerInput;

    protected override void Start()
    {
        base.Start();
        _playerInput = _playerTransform.GetComponent<PlayerInput>();
        _interactionPrompt = GetComponentInChildren<ButtonPrompt>();
    }

    private ButtonPromptTypes GetTypeFromCurrentControlScheme(string controlScheme)
    => controlScheme switch
    {
        "Keyboard&Mouse" => ButtonPromptTypes.Keyboard,
        "Gamepad" => ButtonPromptTypes.Playstation,
        _ => throw new System.NotImplementedException("Unsupported Control Device")
    };

    public override void Highlight()
    {
        _interactionPrompt.ShowPrompt(GetTypeFromCurrentControlScheme(_playerInput.currentControlScheme));
    }

    public override void UnHighlight()
    {
        _interactionPrompt.HidePrompt();
    }
}
