using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public class SnapshotHandler : MonoBehaviour
{
    private EventInstance currentSnapshot;

    /// <summary>
    /// Activates an FMOD snapshot by its path.
    /// </summary>
    /// <param name="snapshotPath">The FMOD snapshot path (e.g., "snapshot:/PlayerDeath").</param>
    public void ActivateSnapshot(string snapshotPath)
    {
        // Stop the current snapshot if one is active
        if (currentSnapshot.isValid())
        {
            currentSnapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentSnapshot.release();
        }

        // Create and start the new snapshot
        currentSnapshot = RuntimeManager.CreateInstance(snapshotPath);
        currentSnapshot.start();
    }

    /// <summary>
    /// Deactivates the currently active snapshot, if any.
    /// </summary>
    public void DeactivateSnapshot()
    {
        if (currentSnapshot.isValid())
        {
            currentSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentSnapshot.release();
        }
    }

    private void OnDestroy()
    {
        // Ensure snapshot is stopped and released when the script is destroyed
        DeactivateSnapshot();
    }
}
