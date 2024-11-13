using UnityEngine;
using UnityEngine.InputSystem;

public class Merchant : Interactable
{
    [SerializeField]
    private float _promptDistance = 1f;
    private Transform _playerTransform;
    private ButtonPrompt _interactionPrompt;
    private PlayerInput _playerInput;
    private RoomManager _roomManager;
    private bool _hasShopped;

    void Start()
    {
        _playerTransform = FindFirstObjectByType<Movement>().transform;
        _playerInput = _playerTransform.GetComponent<PlayerInput>();
        _interactionPrompt = GetComponentInChildren<ButtonPrompt>();
        _roomManager = FindFirstObjectByType<RoomManager>();
    }

    void Update()
    {
        if (_hasShopped)
            return;

        float distance = Vector3.Distance(_playerTransform.position, transform.position);
        if (IsInteractable = distance <= _promptDistance)
            _interactionPrompt.ShowPrompt(GetTypeFromCurrentControlScheme(_playerInput.currentControlScheme));
        else
            _interactionPrompt.HidePrompt();
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
        _roomManager.OpenUpgradeMenu();
        _hasShopped = true;
        IsInteractable = false;
        _interactionPrompt.HidePrompt();
    }
}
