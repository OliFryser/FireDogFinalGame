using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class UIButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private EventReference buttonClickSound;
    [SerializeField] private EventReference buttonHoverSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!buttonClickSound.IsNull)
        {
            RuntimeManager.PlayOneShot(buttonClickSound);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!buttonHoverSound.IsNull)
        {
            RuntimeManager.PlayOneShot(buttonHoverSound);
        }
    }
}
