using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueActivator : MonoBehaviour, ISubmitHandler, IPointerClickHandler
{
    private DialogueManager _dialogueManager;
    void Awake()
    {
        _dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _dialogueManager.OnDialogClick();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _dialogueManager.OnDialogClick();
    }
}
