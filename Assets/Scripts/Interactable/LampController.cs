using UnityEngine;
using FMODUnity;

public class LampController : Interactable
{
    [Header("Lamp Sprites")]
    [Tooltip("Assign the GameObject representing the lamp in the 'off' state.")]
    public GameObject offLamp;

    [Tooltip("Assign the GameObject representing the lamp in the 'on' state.")]
    public GameObject onLamp;

    [Header("Initial State")]
    [Tooltip("Set to true if the lamp should start in the 'on' state.")]
    public bool startOn = false;

    private bool isOn;

    protected override void Start()
    {
        base.Start();

        // Initialize the lamp's state based on the 'startOn' value
        isOn = startOn;

        if (offLamp != null && onLamp != null)
        {
            offLamp.SetActive(!isOn);
            onLamp.SetActive(isOn);
        }
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
        // Do nothing here, since lamp is an easter egg
    }

    public override void UnHighlight()
    {
        // Do nothing here
    }
}
