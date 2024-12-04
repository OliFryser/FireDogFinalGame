using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class Level7 : MonoBehaviour
{
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
        FindAnyObjectByType<Flashlight>().TurnOnFlashlight();
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
