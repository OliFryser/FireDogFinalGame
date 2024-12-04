using UnityEngine;
using FMODUnity;

public class SFX_Footsteps : MonoBehaviour
{
    [Header("Footstep Sounds")]
    public EventReference footstepEvent;

    [Header("Footstep Start/Stop Sounds")]
    public EventReference footstepStartEvent;
    public EventReference footstepStopEvent;

    /// <summary>
    /// Plays a single footstep sound. Intended to be called via animation events.
    /// </summary>
    public void footstep() // Ensure this method name matches the animation event
    {
        FMOD.Studio.EventInstance footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
        footstepInstance.start();
        footstepInstance.release();
    }

    /// <summary>
    /// Plays the footstep start sound.
    /// </summary>
    public void PlayFootstepStart()
    {
        RuntimeManager.PlayOneShot(footstepStartEvent, transform.position);
    }

    /// <summary>
    /// Plays the footstep stop sound.
    /// </summary>
    public void PlayFootstepStop()
    {
        RuntimeManager.PlayOneShot(footstepStopEvent, transform.position);
    }
}
