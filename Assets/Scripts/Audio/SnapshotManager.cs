using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class SnapshotManager : MonoBehaviour
{
    private static SnapshotManager _instance;
    public static SnapshotManager Instance => !_instance ? Initialize() : _instance;
    
    private EventInstance _deathSnapshotInstance;

    public static SnapshotManager Initialize()
    {
        GameObject snapshotManager = new GameObject("SnapshotManager");
        snapshotManager.AddComponent<SnapshotManager>();
        return snapshotManager.GetComponent<SnapshotManager>();
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
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
