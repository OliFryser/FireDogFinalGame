using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class DialoguePlayer : MonoBehaviour
{

    [SerializeField]
    private DialogueLineAdapter _dialogueLineAdapter;

    private Queue<DialogueLine> _dialogueSequenceQueue;

    private Action _onCompleted;

    private bool _isDialogueActive = false;

    public void StartDialog(DialogueSequence dialogueSequence, Action onDialogSequenceCompleted)
    {
        _onCompleted = onDialogSequenceCompleted;
        _dialogueSequenceQueue = new Queue<DialogueLine>(dialogueSequence.Lines);
        _isDialogueActive = true;
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        _dialogueLineAdapter.PlayDialogueLines(_dialogueSequenceQueue.Dequeue(), OnDialogLineCompleted);
    }

    private void OnDialogLineCompleted()
    {
        if (_dialogueSequenceQueue.Count == 0)
        {
            _dialogueLineAdapter.HideDisplay();
            _isDialogueActive = false;
            _onCompleted?.Invoke();
            return;
        }
        ShowNextLine();
    }

    public void OnDialogClick()
    {
        if (_isDialogueActive)
        {
            _dialogueLineAdapter.OnDialogClick();
        }
    }
}
