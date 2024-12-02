using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;

namespace Dialogue
{
  public class DialogueLineAdapter : MonoBehaviour
  {
    private DialogueLine _dialogueLine;

    [SerializeField]
    private DialogueDisplay _dialogueDisplay;

    private Queue<string> _lineQueue;

    private Action _onCompleted;

    public void PlayDialogueLines(DialogueLine dialogueLine, Action onCompleted)
    {
      _onCompleted = onCompleted;

      _dialogueLine = dialogueLine;
      _dialogueDisplay.gameObject.SetActive(true);
      _dialogueDisplay.SetDialogueSpeaker(_dialogueLine.Speaker);

      var lines =
        TextSplitter
          .SplitTextToFit(
            _dialogueLine.DialogContent,
            _dialogueDisplay.DialogueTextDisplay,
            _dialogueDisplay.DialogueTextDisplayTransform);

      _lineQueue = new Queue<string>(lines);
      PlayNextLineInDisplay();
    }

    private void PlayNextLineInDisplay()
    {
      _dialogueDisplay.PlayLine(_lineQueue.Dequeue(), OnDialogLinePlayed);
    }

    private void OnDialogLinePlayed()
    {
      if (!_lineQueue.Any())
      {
        _onCompleted();
        return;
      }
      PlayNextLineInDisplay();
    }

    public void HideDisplay()
    {
      _dialogueDisplay.gameObject.SetActive(false);
    }

    public void OnDialogClick()
    {
      _dialogueDisplay.OnDialogClick();
    }

  }
}