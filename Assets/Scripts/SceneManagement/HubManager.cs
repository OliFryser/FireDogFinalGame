using System;
using System.Collections;
using Dialogue;
using UnityEngine;

namespace SceneManagement
{
    public class HubManager : MonoBehaviour
    {
        private HubDoor _door;
        private DialogueManager _dialogueManager;
        private InputLock _inputLock;
        private PersistentPlayerStats _persistentPlayerStats;

        private void Awake()
        {
            _door = FindAnyObjectByType<HubDoor>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
            _inputLock = FindAnyObjectByType<InputLock>();
        }

        private void Start()
        {
            if (_persistentPlayerStats.Deaths == 0)
            {
                _dialogueManager.PlayHubDialogue(0, () =>
                {
                    _door.OpenDoor();
                });
                StartCoroutine(LockInputDelayed());
            }
            else 
                _door.OpenDoor();
        }
        // Necessary when calling inputLock in start method, since it seems to get enabled later
        private IEnumerator LockInputDelayed()
        {
            yield return new WaitForSeconds(0.5f);
            _inputLock.UnlockInput();
            _inputLock.LockInput();
        }

        public void PlayMerchantDialogue(Action action)
        {
            _dialogueManager.PlayHubDialogue(_persistentPlayerStats.Deaths, action);
        }
    }
}
