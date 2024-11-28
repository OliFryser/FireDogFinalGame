using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Animator fadeAnimator;
    private InputLock _inputLock;
    public float fadeDuration = 1.0f; // Duration of the fade animation

    void Start()
    {
        // Find the InputLock script in the scene
        _inputLock = FindAnyObjectByType<InputLock>();
    }

    public void FadeToBlack()
    {
        if (_inputLock != null)
        {
            _inputLock.LockInput();
        }

        fadeAnimator.SetTrigger("FadeOut");
    }

    public void FadeFromBlack()
    {
        fadeAnimator.SetTrigger("FadeIn");

        // Unlock input after the fade duration
        StartCoroutine(FadeTimer(fadeDuration));
    }

    private void UnlockInputAfterFade()
    {
        if (_inputLock != null)
        {
            _inputLock.UnlockInput();
        }
    }

    IEnumerator FadeTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        UnlockInputAfterFade();
    }
}
