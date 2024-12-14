using System;
using System.Collections;
using Dialogue;
using Player;
using UnityEngine;

namespace SceneManagement
{
    public class HubManager : MonoBehaviour
    {
        private HubDoor _door;
        private DialogueManager _dialogueManager;
        private InputLock _inputLock;
        private PersistentPlayerStats _persistentPlayerStats;
        private bool _initialized;

        private void Start()
        {
            _door = FindAnyObjectByType<HubDoor>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _persistentPlayerStats = FindAnyObjectByType<PersistentPlayerStats>();
            _inputLock = FindAnyObjectByType<InputLock>();

            // Ensure SnapshotManager is initialized
            if (SnapshotManager.Instance == null)
            {
                SnapshotManager.Initialize();
            }
            StartCoroutine(DelayedStopDeathSnapshot());
        }

        private IEnumerator DelayedStopDeathSnapshot()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.2f);

            // Attempt to stop the snapshot
            StopDeathSnapshot();

            // If still active, try again after another wait
            yield return new WaitForSeconds(0.5f);
            if (SnapshotManager.Instance != null && SnapshotManager.Instance.IsDeathSnapshotActive())
            {
                SnapshotManager.Instance.DeactivateDeathSnapshot();
            }
        }

        // Only does something on first frame
        private void Update()
        {
            if (_initialized) return;
            _initialized = true;
            if (_persistentPlayerStats.Deaths == 0)
            {
                _dialogueManager.PlayHubDialogue(0, _door.OpenDoor);
                StartCoroutine(LockInputDelayed());
            }
            else

            {
                _door.OpenDoor();
            }
        }

        // Necessary when calling inputLock in start method, since it seems to get enabled later
        private IEnumerator LockInputDelayed()
        {
            yield return new WaitForSeconds(0.1f);
            _inputLock.UnlockInput();
            _inputLock.LockInput();
        }

        public void PlayMerchantDialogue(Action action)
        {
            _dialogueManager.PlayHubDialogue(_persistentPlayerStats.Deaths, action);
        }

        private void StopDeathSnapshot()
        {
            if (SnapshotManager.Instance == null)
            {
                Debug.LogError("SnapshotManager is not initialized. Cannot stop Death Snapshot.");
                return;
            }

            if (SnapshotManager.Instance.IsDeathSnapshotActive())
            {
                SnapshotManager.Instance.DeactivateDeathSnapshot();
            }
        }
    }

}
