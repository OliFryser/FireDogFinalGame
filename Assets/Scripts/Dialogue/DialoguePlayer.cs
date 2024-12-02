using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogue
{
    public class DialoguePlayer : MonoBehaviour
    {

        [SerializeField]
        private DialogueLineAdapter _dialogueLineAdapter;

        [SerializeField]
        private DialogueActivator _dialogActivator;
        
        private Queue<DialogueLine> _dialogueSequenceQueue;
        
        private InputLock _inputLock;

        private PlayerCameraFollow _cameraFollow;

        private Action _onCompleted;

        private void Start()
        {
            _inputLock = FindAnyObjectByType<InputLock>();
            _cameraFollow = FindAnyObjectByType<PlayerCameraFollow>();
        }
        
        public void StartDialog(DialogueSequence dialogueSequence, Action onDialogSequenceCompleted)
        {
            _onCompleted = onDialogSequenceCompleted;
            _dialogueSequenceQueue = new(dialogueSequence.Lines);
            _dialogActivator.gameObject.SetActive(true);
            if (_inputLock != null)
            {
                Debug.Log(_inputLock.name);
                _inputLock.LockInput();
            }
            if (_cameraFollow != null)
                _cameraFollow.MoveCameraForDialog();
            
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
                _dialogActivator.gameObject.SetActive(false);
                if(_inputLock)
                    _inputLock.UnlockInput();
                if (_cameraFollow != null)
                    _cameraFollow.MoveCameraBackAfterDialogue();
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
}
