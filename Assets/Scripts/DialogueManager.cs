using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using FMODUnity;
using System;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;
    public float TypingSpeed = 0.05f;
    public EventReference CharacterSoundEvent;

    private List<string> _dialogueLines;
    private int _currentLineIndex = 0;
    private bool _isTyping = false;
    private bool _dialogueStarted = false;

    private HubDoor _hubDoor;

    private void Awake()
    {
        _hubDoor = FindAnyObjectByType<HubDoor>();
    }

    void Start()
    {
        DialoguePanel.SetActive(false);
    }

    public void StartDialogue(List<string> lines)
    {
        _dialogueLines = lines;
        _currentLineIndex = 0;
        _dialogueStarted = true;
        DialoguePanel.SetActive(true);
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (_currentLineIndex < _dialogueLines.Count)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText(_dialogueLines[_currentLineIndex]));
            _currentLineIndex++;
        }
        else
        {
            EndDialogue();
            StartCoroutine(EndScene());
        }
    }

    private IEnumerator EndScene()
    {
        _hubDoor.OpenDoor();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    // Simplified coroutine for typewriter effect
    private IEnumerator TypeText(string line)
    {
        _isTyping = true;
        DialogueText.text = "";                // Clear the text box

        foreach (char letter in line.ToCharArray())
        {
            DialogueText.text += letter;
            PlayCharacterSound();              // Play FMOD sound for each character
            yield return new WaitForSeconds(TypingSpeed);
        }

        _isTyping = false;
    }

    // Method to play FMOD sound for each character
    private void PlayCharacterSound()
    {
        if (CharacterSoundEvent.IsNull) return;
        FMOD.Studio.EventInstance soundInstance = RuntimeManager.CreateInstance(CharacterSoundEvent);
        soundInstance.start();
        soundInstance.release();
    }

    // Skip to display full line if typing is in progress
    public void SkipToFullText()
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            DialogueText.text = _dialogueLines[_currentLineIndex - 1];
            _isTyping = false;
        }
    }

    // End the dialogue by hiding the panel
    private void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        _dialogueStarted = false;
    }

    public void OnDialogClick()
    {
        if (!_dialogueStarted)
        {
            StartDialogue(new List<string> {
                "This the place? Shouldn't they\nwant a couple of 100 renovators",
                "or a bulldozer? Where was\nthat letter again?",
                "\"My deepest gratitude for\naccepting this undertaking.",
                "The remainder of your payment\nshall be rendered upon",
                "the completion of your duties.\nAs previously noted,",
                "the manor has languished in\nneglect for many years,",
                "and you would do well to prepare\nyourself for a rather",
                "persistent infestation.\"",
                "An infestation? Ha. Nothing ever\nsurvived my mop and strong",
                "cleaning arm!"
            });
        }
        else if (DialoguePanel.activeSelf)
        {
            if (_isTyping)
            {
                SkipToFullText();
            }
            else
            {
                ShowNextLine();
            }
        }
    }
}
