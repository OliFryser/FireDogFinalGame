using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Animator fadeAnimator;
    public Movement playerMovement;

    public void FadeToBlack()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void FadeFromBlack()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }
}
