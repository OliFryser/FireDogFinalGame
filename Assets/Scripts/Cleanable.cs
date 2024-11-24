using NUnit.Framework;
using UnityEngine;

public class Cleanable : Interactable
{
    [SerializeField]
    private Material _defaultMaterial;

    private Material _outlinedMaterial;

    private SpriteRenderer _spriteRenderer;

    private PlayerStats _playerStats;

    public override void Interact()
    {

        Debug.Log("Cleaned!");
        base.Interact();
        _playerStats.AddCoins(_playerStats.CleaningReward);
    }

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _outlinedMaterial = _spriteRenderer.material;
        _spriteRenderer.material = _defaultMaterial;
        _playerStats = FindAnyObjectByType<PlayerStats>();
    }

    public override void Highlight()
    {
        _spriteRenderer.material = _outlinedMaterial;
    }

    public override void UnHighlight()
    {
        _spriteRenderer.material = _defaultMaterial;
    }
}
