using UnityEngine;
using System.Collections;

public class CleanupManager : MonoBehaviour
{
    public ScreenFade screenFade; // Reference to your ScreenFade script
    public AudioSource cleaningSound; // Drag your cleaning sound here

    private void Start()
    {
        // Optionally, find the ScreenFade script automatically if not assigned
        if (screenFade == null)
            screenFade = FindAnyObjectByType<ScreenFade>();

        // Add cleaning sound setup if needed
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
        screenFade.FadeToBlack();

        // Wait for the fade-out to complete (adjust timing to match your fade animation)
        yield return new WaitForSeconds(0f);

        // Step 2: Play cleaning sound
        if (cleaningSound != null)
            cleaningSound.Play();

        // Step 3: Disable dirty assets
        GameObject[] dirtyAssets = GameObject.FindGameObjectsWithTag("DirtyAsset");
        foreach (GameObject asset in dirtyAssets)
        {
            asset.SetActive(false);
        }

        // Wait for cleaning sound duration (or another appropriate delay)
        yield return new WaitForSeconds (0.0f);

        // Step 4: Fade back in
        screenFade.FadeFromBlack();
    }
}
