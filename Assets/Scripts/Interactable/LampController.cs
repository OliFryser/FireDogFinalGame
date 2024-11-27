using UnityEngine;
using FMODUnity;
using UnityEngine.Video;

public class LampController : Interactable
{
    [Header("Lamp Sprites")]
    [Tooltip("Assign the GameObject representing the lamp in the 'off' state.")]
    public GameObject offLamp;

    [Tooltip("Assign the GameObject representing the lamp in the 'on' state.")]
    public GameObject onLamp;

    private bool isOn = false;

    protected override void Start()
    {
        base.Start();
        offLamp.SetActive(true);
        onLamp.SetActive(false);
    }

    public void ToggleLamp()
    {
        RuntimeManager.PlayOneShot("event:/Environment/Lamp_switch");
        isOn = !isOn;

        if (offLamp != null && onLamp != null)
        {
            offLamp.SetActive(!isOn);
            onLamp.SetActive(isOn);
        }
    }

    public override void Interact()
    {
        ToggleLamp();
    }

    public override void Highlight()
    {
        // Do nothing here, since lamp is an easteregg
    }

    public override void UnHighlight()
    {
        // Do nothing here
    }
}
