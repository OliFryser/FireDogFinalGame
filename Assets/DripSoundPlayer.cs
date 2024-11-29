using UnityEngine;
using FMODUnity;

public class DripSoundPlayer : MonoBehaviour
{
    [SerializeField] private EventReference dripSound;

    public void PlayDripSound()
    {
        if (!dripSound.IsNull)
        {
            RuntimeManager.PlayOneShot(dripSound); // Play the FMOD drip sound
        }
        else
        {
            Debug.LogWarning("Drip sound FMOD event is not assigned.");
        }
    }
}
