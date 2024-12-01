using UnityEngine;
using System.Collections;

public class InvincibilityManager : MonoBehaviour
{
    private bool _isInvincible = false;
    public bool IsInvincible => _isInvincible;

    [SerializeField] private SpriteRenderer _spriteRenderer; // Assign the player's sprite renderer in the inspector
    [SerializeField] private float blinkInterval = 0.2f; // Interval between blinks
    [SerializeField, Range(0f, 1f)] private float blinkAlpha = 0.5f; // Adjust in Inspector for transparency
    [SerializeField, Range(0f, 1f)] private float blinkToBlackAmount = 0.8f; // Adjust in Inspector to control blackness
    [SerializeField] private float invincibilityDuration = 1.5f;

    private Coroutine _blinkCoroutine; // To keep track of the active BlinkEffect coroutine
    private Color _originalColor; // Store the original color of the sprite

    private void Awake()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        _originalColor = _spriteRenderer.color; // Store the original color
    }

    public IEnumerator MakeInvincible(float? duration = null)
    {
        float time = duration ?? invincibilityDuration; // Use passed duration or default
        _isInvincible = true;

        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _spriteRenderer.enabled = true;
            _spriteRenderer.color = _originalColor; // Reset to original color
        }

        _blinkCoroutine = StartCoroutine(BlinkEffect(time));
        yield return new WaitForSeconds(time);

        _isInvincible = false;
        _spriteRenderer.enabled = true;
        _spriteRenderer.color = _originalColor; // Ensure color is reset after invincibility ends
    }

    private IEnumerator BlinkEffect(float duration)
    {
        float elapsed = 0f;
        // Interpolate between original color and black based on blinkToBlackAmount
        Color blinkColor = Color.Lerp(_originalColor, Color.black, blinkToBlackAmount);
        blinkColor.a = blinkAlpha; // Use the specified alpha for transparency

        while (elapsed < duration)
        {
            _spriteRenderer.color = _spriteRenderer.color == _originalColor ? blinkColor : _originalColor;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        _spriteRenderer.color = _originalColor; // Reset to original color
    }
}

