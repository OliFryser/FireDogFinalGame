using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using FMODUnity;
using System;
using System.Linq;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private DialogueLineAdapter _dialogueLineAdapter;

    private Queue<DialogueLine> _dialogueSequenceQueue;

    private Action _onCompleted;

    public void StartDialog(DialogueSequence dialogueSequence, Action onDialogSequenceCompleted)
    {
        _onCompleted = onDialogSequenceCompleted;
        _dialogueSequenceQueue = new(dialogueSequence.Lines);
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        _dialogueLineAdapter.PlayDialogueLines(_dialogueSequenceQueue.Dequeue(), OnDialogLineCompleted);
    }

    private void OnDialogLineCompleted()
    {
        if (!_dialogueSequenceQueue.Any())
        {
            _dialogueLineAdapter.HideDisplay();
            _onCompleted();
            return;
        }
        ShowNextLine();
    }

    public void OnDialogClick()
    {
        _dialogueLineAdapter.OnDialogClick();
    }
}
