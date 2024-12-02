using Lib;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{
    [SerializeField]
    private ButtonPrompts _buttonPrompt;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowPrompt(ButtonPromptTypes buttonPromptType)
    {
        switch (buttonPromptType)
        {
            case ButtonPromptTypes.Playstation:
                _spriteRenderer.sprite = _buttonPrompt.PlaystationPrompt;
                break;
            case ButtonPromptTypes.Keyboard:
                _spriteRenderer.sprite = _buttonPrompt.KeyboardPrompt;
                break;
        }
        _spriteRenderer.enabled = true;
    }

    public void HidePrompt()
    {
        _spriteRenderer.enabled = false;
    }
}
