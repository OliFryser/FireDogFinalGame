using UnityEngine;
using System.Collections;

public class CleanupManager : MonoBehaviour
{
    public ScreenFade screenFade; // Reference to your ScreenFade script
    public AudioSource cleaningSound; // Drag your cleaning sound here
    
    private void Start()
    {
        // Optionally, find scripts automatically if not assigned
        if (screenFade == null)
            screenFade = FindAnyObjectByType<ScreenFade>();

        if (cleaningSound == null)
            Debug.LogError("Cleaning sound not assigned!");
    }

    public void TriggerCleanup()
    {
        StartCoroutine(CleanupSequence());
    }

    private IEnumerator CleanupSequence()
    {
        if (screenFade != null)
        {
            screenFade.FadeToBlack();
            yield return new WaitForSeconds(1.0f);
        }

        if (cleaningSound != null)
        {
            cleaningSound.Play();
            yield return new WaitForSeconds(cleaningSound.clip.length);
        }

        if (screenFade != null)
        {
            screenFade.FadeFromBlack();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
