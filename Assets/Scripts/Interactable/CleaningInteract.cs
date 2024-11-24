using UnityEngine;
using UnityEngine.InputSystem;

public class CleaningInteract : Interactable
{
    [SerializeField]
    protected float _promptDistance = 1f;
    protected Transform _playerTransform;
    protected ButtonPrompt _interactionPrompt;
    protected PlayerInput _playerInput;
    protected RoomManager _roomManager;

    protected SpriteRenderer _spriteRenderer;

    protected bool _hasCleaned;
    

    [SerializeField]
    protected Sprite _cleanSprite;

    protected void Start()
    {
        _playerTransform = FindFirstObjectByType<Movement>().transform;
        _playerInput = _playerTransform.GetComponent<PlayerInput>();
        _interactionPrompt = GetComponentInChildren<ButtonPrompt>();
        _roomManager = FindFirstObjectByType<RoomManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        if (_hasCleaned)
            return;
        float distance = Vector3.Distance(_playerTransform.position, transform.position);
        if (IsInteractable = distance <= _promptDistance)
            _interactionPrompt.ShowPrompt(GetTypeFromCurrentControlScheme(_playerInput.currentControlScheme));
        else
            _interactionPrompt.HidePrompt();
    }

    protected ButtonPromptTypes GetTypeFromCurrentControlScheme(string controlScheme)
    => controlScheme switch
    {
        "Keyboard&Mouse" => ButtonPromptTypes.Keyboard,
        "Gamepad" => ButtonPromptTypes.Playstation,
        _ => throw new System.NotImplementedException("Unsupported Control Device")
    };


    public override void Interact()
    {
        IsInteractable = false;
        _hasCleaned = true;
        _spriteRenderer.sprite = _cleanSprite;
        _interactionPrompt.HidePrompt();
    }
}
