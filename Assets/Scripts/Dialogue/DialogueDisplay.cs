using FMODUnity;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _speakerSpriteDisplay;

        [SerializeField]
        private TMP_Text _speakerName;

        public TMP_Text DialogueTextDisplay;
        public RectTransform DialogueTextDisplayTransform;

        private string _line;

        public bool IsTyping;

        [SerializeField]
        private float _typingSpeed;

        private Action _onCompleted;

        public void SetDialogueSpeaker(Speaker speaker)
        {
            _speakerName.text = speaker.Name;
            _speakerSpriteDisplay.sprite = speaker.Sprite;
            _speakerSpriteDisplay.SetNativeSize();
        }

        public void PlayLine(string line, Action onDialogLinePlayed)
        {
            _onCompleted = onDialogLinePlayed;
            IsTyping = true;
            DialogueTextDisplay.text = "";
            _line = line;
            StartCoroutine(TypeLine());
        }

        private IEnumerator TypeLine()
        {
            foreach (char letter in _line.ToCharArray())
            {
                DialogueTextDisplay.text += letter;
                PlayCharacterSound();
                yield return new WaitForSeconds(_typingSpeed);
            }
            IsTyping = false;
        }

        private void SkipToEnd()
        {
            StopAllCoroutines();
            DialogueTextDisplay.text = _line;
            IsTyping = false;
            RuntimeManager.PlayOneShot("event:/UI/GUI/Toggle");
        }

        private void PlayCharacterSound()
        {
            // if (CharacterSoundEvent.IsNull) return;
            // FMOD.Studio.EventInstance soundInstance = RuntimeManager.CreateInstance(CharacterSoundEvent);
            // soundInstance.start();
            // soundInstance.release();
        }

        internal void OnDialogClick()
        {
            if (IsTyping)
                SkipToEnd();
            else
                _onCompleted();
            RuntimeManager.PlayOneShot("event:/UI/GUI/Toggle");

        }
    }
}
