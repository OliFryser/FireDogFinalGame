using System;
using Dialogue;
using UnityEngine;

namespace SceneManagement
{
    public class HubManager : MonoBehaviour
    {
        private HubDoor _door;
        private DialogueManager _dialogueManager;
        private PersistentPlayerStats _persistentPlayerStats;

        private void Awake()
        {
            _door = FindAnyObjectByType<HubDoor>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
        }

        private void Start()
        {
            if (_persistentPlayerStats.Deaths == 0)
            {
                _dialogueManager.PlayHubDialogue(0, () =>
                {
                    _door.OpenDoor();
                });
            }
            else 
                _door.OpenDoor();
        }

        public void PlayMerchantDialogue(Action action)
        {
            _dialogueManager.PlayHubDialogue(_persistentPlayerStats.Deaths, action);
        }
    }
}
