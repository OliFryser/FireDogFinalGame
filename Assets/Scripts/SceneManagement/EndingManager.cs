using System.Collections;
using Dialogue;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private DialogueSequence _startingDialogue;
    [SerializeField] private DialogueSequence _firstChoiceSequence;
    [SerializeField] private DialogueSequence _secondChoiceSequence;
    [SerializeField] private GameObject _choices;
    [SerializeField] private EndingMerchant _endingMerchant;
    
    private DialoguePlayer _dialoguePlayer;

    private void Start()
    {
        _dialoguePlayer = FindAnyObjectByType<DialoguePlayer>();
        _dialoguePlayer.StartDialog(_startingDialogue, OnStartingDialogCompleted);
    }

    private void OnStartingDialogCompleted()
    {
        _choices.SetActive(true);
    }

    public void OnFirstChoice()
    {
        _choices.SetActive(false);
        _endingMerchant.SwitchToWizard();
        _dialoguePlayer.StartDialog(_firstChoiceSequence, OnLastSequenceCompleted);
    }

    public void OnSecondChoice()
    {
        _choices.SetActive(false);
        _dialoguePlayer.StartDialog(_secondChoiceSequence, OnLastSequenceCompleted);
    }

    private void OnLastSequenceCompleted()
    {
        StartCoroutine(EndingSequence());
    }

    private IEnumerator EndingSequence()
    {
        yield return new WaitForSeconds(1f);
        // TODO load credits
        SceneManager.LoadScene(0);
    }
}
