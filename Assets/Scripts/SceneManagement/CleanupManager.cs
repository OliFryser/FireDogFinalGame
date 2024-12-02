using UnityEngine;
using System.Collections;

public class CleanupManager : MonoBehaviour
{
    public ScreenFade screenFade; // Reference to your ScreenFade script
    public AudioSource cleaningSound; // Drag your cleaning sound here
    public CleanupDisable cleanupDisable; // Reference to CleanupDisable script

    private void Start()
    {
        // Optionally, find scripts automatically if not assigned
        if (screenFade == null)
            screenFade = FindAnyObjectByType<ScreenFade>();
        if (cleanupDisable == null)
            cleanupDisable = FindFirstObjectByType<CleanupDisable>();

        if (cleaningSound == null)
            Debug.LogError("Cleaning sound not assigned!");
    }

    public void TriggerCleanup()
    {
        StartCoroutine(CleanupSequence());
    }

    private IEnumerator CleanupSequence()
    {
        // Step 1: Fade to black
        if (screenFade != null)
        {
            screenFade.FadeToBlack();
            yield return new WaitForSeconds(1.0f); // Adjust fade duration
        }

        // Step 2: Play cleaning sound
        if (cleaningSound != null)
        {
            cleaningSound.Play();
            yield return new WaitForSeconds(cleaningSound.clip.length);
        }

        // Step 3: Use CleanupDisable to disable dirty assets
        if (cleanupDisable != null)
        {
            cleanupDisable.DisableObjectsWithTag("DirtyAsset");
        }

        // Step 4: Fade back in
        if (screenFade != null)
        {
            screenFade.FadeFromBlack();
            yield return new WaitForSeconds(1.0f); // Adjust fade duration
        }
    }
}
