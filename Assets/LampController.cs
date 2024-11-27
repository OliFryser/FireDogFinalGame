using UnityEngine;
using FMODUnity;

public class LampController : MonoBehaviour
{
    [Header("Lamp Sprites")]
    [Tooltip("Assign the GameObject representing the lamp in the 'off' state.")]
    public GameObject offLamp;

    [Tooltip("Assign the GameObject representing the lamp in the 'on' state.")]
    public GameObject onLamp;

    [Header("Interaction Settings")]
    [Tooltip("Key used to toggle the lamp.")]
    public KeyCode interactionKey = KeyCode.E;

    private bool isOn = false;
    private bool playerInRange = false;

    void Start()
    {
        if (offLamp != null && onLamp != null)
        {
            offLamp.SetActive(true);
            onLamp.SetActive(false);
        }
        else
        {
            // Optionally handle the case where lamp references are not assigned
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            ToggleLamp();
        }
    }

    /// <summary>
    /// Toggles the lamp between on and off states.
    /// </summary>
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
