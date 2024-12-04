using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ReverbZone : MonoBehaviour
{
    [Header("FMOD Snapshot Settings")]
    [SerializeField]
    private EventReference snapshotEvent;

    private EventInstance snapshotInstance;

    [Header("Reverb Activation")]
    [SerializeField]
    private bool activateSnapshot; // True to activate, False to deactivate

    private void Start()
    {
        if (!snapshotEvent.IsNull)
        {
            snapshotInstance = RuntimeManager.CreateInstance(snapshotEvent);
        }
        else
        {
            Debug.LogWarning("ReverbZone: Snapshot event reference is not assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (activateSnapshot)
            {
                ActivateSnapshot();
            }
            else
            {
                DeactivateSnapshot();
            }
        }
    }

    private void ActivateSnapshot()
    {
        if (snapshotInstance.isValid())
        {
            snapshotInstance.start();
            Debug.Log("ReverbZone: Snapshot activated.");
        }
    }

    private void DeactivateSnapshot()
    {
        if (snapshotInstance.isValid())
        {
            snapshotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            snapshotInstance.release();
            Debug.Log("ReverbZone: Snapshot deactivated.");
        }
    }
}
