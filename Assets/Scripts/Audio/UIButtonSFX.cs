using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class UIButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler, ISubmitHandler
{
    [SerializeField] private EventReference buttonClickSound;
    [SerializeField] private EventReference buttonHoverSound;

    private bool isUsingController = false;

    private void Update()
    {
        // Detect if any controller is connected
        if (Input.GetJoystickNames().Length > 0)
        {
            isUsingController = true;
        }
        else
        {
            isUsingController = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Only play click sound if using mouse
        if (!isUsingController && !buttonClickSound.IsNull)
        {
            RuntimeManager.PlayOneShot(buttonClickSound);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Only play hover sound if using mouse
        if (!isUsingController && !buttonHoverSound.IsNull)
        {
            RuntimeManager.PlayOneShot(buttonHoverSound);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        // Play hover sound when selected via controller
        if (isUsingController && !buttonHoverSound.IsNull)
        {
            RuntimeManager.PlayOneShot(buttonHoverSound);
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {
        // Play click sound when submitted via controller
        if (isUsingController && !buttonClickSound.IsNull)
        {
            RuntimeManager.PlayOneShot(buttonClickSound);
        }
    }
}
