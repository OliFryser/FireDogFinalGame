using UnityEngine;
using FMODUnity;

public class SFX_Footsteps : MonoBehaviour
{
    public EventReference footstepEvent;

    public void footstep()
    {
        // Create an instance of the footstep event
        FMOD.Studio.EventInstance footstepInstance = RuntimeManager.CreateInstance(footstepEvent);


        // Start the event
        footstepInstance.start();

        // Release the instance after it's done to free up resources
        footstepInstance.release();
    }
}
