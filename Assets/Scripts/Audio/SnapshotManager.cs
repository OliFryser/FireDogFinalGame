using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SnapshotManager : MonoBehaviour
{
    public static SnapshotManager Instance { get; private set; }

    private EventInstance _deathSnapshotInstance;

    public static void Initialize()
    {
        if (Instance == null)
        {
            GameObject snapshotManager = new GameObject("SnapshotManager");
            snapshotManager.AddComponent<SnapshotManager>();
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ActivateDeathSnapshot()
    {
        if (_deathSnapshotInstance.isValid())
        {
            _deathSnapshotInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _deathSnapshotInstance.release();
        }

        _deathSnapshotInstance = RuntimeManager.CreateInstance("snapshot:/Death");
        _deathSnapshotInstance.start();
    }

    public void DeactivateDeathSnapshot()
    {
        if (_deathSnapshotInstance.isValid())
        {
            _deathSnapshotInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _deathSnapshotInstance.release();
        }
    }

    public bool IsDeathSnapshotActive()
    {
        return _deathSnapshotInstance.isValid();
    }
}
