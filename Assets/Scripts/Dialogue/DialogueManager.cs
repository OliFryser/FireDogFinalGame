using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton Class
namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField]
        private List<DialogueSequence> _hubDialogues;

        [SerializeField]
        private List<RoomDialoguesForRun> _roomDialogues;

        private DialoguePlayer _dialoguePlayer;

        private void Awake()
        {
            var dialogueManagers = FindObjectsByType<DialogueManager>(FindObjectsSortMode.None);
            if (dialogueManagers.Length > 1)
                Destroy(gameObject);
            
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            _dialoguePlayer = FindAnyObjectByType<DialoguePlayer>();
        }

        public void PlayHubDialogue(int index, Action onDialogCompleted)
        {
            if (index < _hubDialogues.Count)
                _dialoguePlayer.StartDialog(_hubDialogues[index], onDialogCompleted);
            else
            {
                Debug.LogWarning("No hub dialog with index: " + index);
                onDialogCompleted();
            }
        }
    
        public void PlayRoomDialogue(int death, int room, Action onDialogCompleted)
        {
            if (death < _roomDialogues.Count && room < _roomDialogues[death].Dialogues.Count)
                _dialoguePlayer.StartDialog(_roomDialogues[death].Dialogues[room], onDialogCompleted);
            else
            {
                Debug.LogWarning("No room dialog with for death: " + death + " and room: " + room);
                onDialogCompleted();
            }
        }

        [System.Serializable]
        public struct RoomDialoguesForRun
        {
            public List<DialogueSequence> Dialogues;
        }
    }
}
