using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;

public class DialogueBox : MonoBehaviour
{
    public GameObject dialoguePanel;              // UI panel for the dialogue box
    public TextMeshProUGUI dialogueText;          // Text component for displaying dialogue
    public float typingSpeed = 0.05f;             // Speed of the typewriter effect
    public EventReference characterSoundEvent;    // FMOD event reference for character sound

    private List<string> dialogueLines;           // List of dialogue lines for sequential display
    private int currentLineIndex = 0;             // Track the current line of dialogue
    private bool isTyping = false;                // Check if typing is still in progress
    private bool dialogueStarted = false;         // Track if dialogue has been initiated

    void Start()
    {
        dialoguePanel.SetActive(false);           // Hide the panel at start
    }

    // Method to initialize and start the dialogue sequence
    public void StartDialogue(List<string> lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        dialogueStarted = true;                   // Mark that the dialogue has started
        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    // Display the current line with a typewriter effect
    private void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            StopAllCoroutines();                  // Stop any ongoing typing effect
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();                        // End dialogue if all lines are shown
        }
    }

    // Simplified coroutine for typewriter effect
    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";                // Clear the text box

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            PlayCharacterSound();              // Play FMOD sound for each character
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // Method to play FMOD sound for each character
    private void PlayCharacterSound()
    {
        if (characterSoundEvent.IsNull) return;   // Check if an event has been set in Inspector
        FMOD.Studio.EventInstance soundInstance = RuntimeManager.CreateInstance(characterSoundEvent);
        soundInstance.start();
        soundInstance.release();                  // Release instance after playing to free up resources
    }

    // Skip to display full line if typing is in progress
    public void SkipToFullText()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex - 1];
            isTyping = false;
        }
    }

    // End the dialogue by hiding the panel
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueStarted = false;
    }

    // Update to handle input for advancing dialogue or starting dialogue on click
    void Update()
    {
        if (!dialogueStarted && Input.GetMouseButtonDown(0))  // Left-click to start dialogue
        {
            // Start dialogue when the screen is clicked for the first time
            StartDialogue(new List<string> {
                "Alright, just gotta clean this \nspooky mansion.No big deal",
                "I mean, it’s not like the couch is \ngonna try to eat me... right?",
                "Who lives here anyway?\nCount Dustula or something?",
                "Okay, pep talk time:You need \nrent money, NOT a panic attack!",
                "Let’s do this!"
            });
        }
        else if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))  // Left-click to advance
        {
            if (isTyping)
            {
                SkipToFullText();  // Complete the line if still typing
            }
            else
            {
                ShowNextLine();    // Otherwise, show the next line
            }
        }
    }
}
