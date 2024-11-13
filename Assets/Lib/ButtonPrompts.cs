using UnityEngine;

[System.Serializable]
public struct ButtonPrompts
{
    public Sprite PlaystationPrompt;
    public Sprite KeyboardPrompt;
}

public enum ButtonPromptTypes
{
    Playstation,
    Keyboard
}