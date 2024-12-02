using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class Level7 : MonoBehaviour
{
    [Header("Flashlight Settings")]
    [SerializeField]
    private Flashlight flashlight;

    [Header("FMOD Snapshot Settings")]
    [SerializeField]
    private EventReference mainStateSnapshot;

    private EventInstance snapshotInstance;

    private void Start()
    {
        // Activate the Flashlight
        ActivateFlashlight();

        // Activate the FMOD "Main State" Snapshot
        ActivateMainStateSnapshot();
    }

    /// <summary>
    /// Activates the flashlight by enabling its light component.
    /// </summary>
    private void ActivateFlashlight()
    {
        if (flashlight != null)
        {
            // Ensure the Flashlight GameObject is active
            if (!flashlight.gameObject.activeSelf)
            {
                flashlight.gameObject.SetActive(true);
                Debug.Log("Flashlight GameObject activated in Level 7.");
            }

            // Turn on the flashlight's light component
            flashlight.TurnOnFlashlight();
            Debug.Log("Flashlight turned on in Level 7.");
        }
        else
        {
            Debug.LogWarning("Flashlight reference is missing in Level7 script.");
        }
    }

    /// <summary>
    /// Activates the FMOD "Main State" snapshot.
    /// </summary>
    private void ActivateMainStateSnapshot()
    {
        if (!mainStateSnapshot.IsNull)
        {
            // Create an instance of the snapshot
            snapshotInstance = RuntimeManager.CreateInstance(mainStateSnapshot);

            // Start the snapshot
            snapshotInstance.start();
            Debug.Log("FMOD 'Main State' snapshot activated in Level 7.");
        }
        else
        {
            Debug.LogWarning("Main State Snapshot EventReference is not assigned in Level7 script.");
        }
    }

    private void OnDestroy()
    {
       
    }
}
