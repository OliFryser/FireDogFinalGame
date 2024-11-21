using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueActivator : MonoBehaviour, ISubmitHandler, IPointerClickHandler
{
    private DialoguePlayer _dialogueManager;
    void Awake()
    {
        _dialogueManager = FindAnyObjectByType<DialoguePlayer>();
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
