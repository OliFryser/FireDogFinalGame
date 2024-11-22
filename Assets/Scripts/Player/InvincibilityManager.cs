using UnityEngine;
using System.Collections;

public class InvincibilityManager : MonoBehaviour
{
    private bool _isInvincible = false;
    public bool IsInvincible => _isInvincible;

    [SerializeField] private SpriteRenderer _spriteRenderer; // Assign the player's sprite renderer in the inspector
    [SerializeField] private float blinkInterval = 0.2f; // Interval between blinks
    [SerializeField] private float invincibilityDuration = 1.5f;


    private Coroutine _blinkCoroutine; // To keep track of the active BlinkEffect coroutine

    private void Awake()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator MakeInvincible(float? duration = null)
    {
        float time = duration ?? invincibilityDuration; // Use passed duration or default
        _isInvincible = true;

        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _spriteRenderer.enabled = true;
        }

        _blinkCoroutine = StartCoroutine(BlinkEffect(time));
        yield return new WaitForSeconds(time);

        _isInvincible = false;
        _spriteRenderer.enabled = true;
    }

    private IEnumerator BlinkEffect(float duration)
    {
        float elapsed = 0f;
        Color originalColor = _spriteRenderer.color;
        Color blinkColor = new Color(1f, 1f, 1f, 0.5f); // Semi-transparent white

        while (elapsed < duration)
        {
            _spriteRenderer.color = _spriteRenderer.color == originalColor ? blinkColor : originalColor;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        _spriteRenderer.color = originalColor; // Reset to original color
    }

}
