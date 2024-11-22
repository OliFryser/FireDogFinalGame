using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    [SerializeField]
    private DialogueSequence _dialogueSequence;
    [SerializeField]
    private float _endDelay = 2f;


    private DialoguePlayer _dialogueManager;
    private HubDoor _hubDoor;

    private void Awake()
    {
        _dialogueManager = FindAnyObjectByType<DialoguePlayer>();
        _hubDoor = FindAnyObjectByType<HubDoor>();
    }

    void Start()
    {
        _dialogueManager.StartDialog(_dialogueSequence, OnDialogSequenceCompleted);
    }

    private void OnDialogSequenceCompleted()
    {
        StartCoroutine(EndHubLevel());
    }

    private IEnumerator EndHubLevel()
    {
        _hubDoor.OpenDoor();
        yield return new WaitForSeconds(_endDelay);
        SceneManager.LoadScene(2);
    }
}
