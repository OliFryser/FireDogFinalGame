using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueActivator : MonoBehaviour, ISubmitHandler, IPointerClickHandler
    {
        private DialoguePlayer _dialogueManager;
        private Button _dialogueButton;

        private void Awake()
        {
            _dialogueManager = FindAnyObjectByType<DialoguePlayer>();
            gameObject.SetActive(false);
            _dialogueButton = GetComponent<Button>();
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                _dialogueButton.Select();
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
}
