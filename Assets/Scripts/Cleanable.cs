using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections;
using Unity.Mathematics;

public class Cleanable : Interactable
{
    [SerializeField]
    private Material _defaultMaterial;

    private Material _outlinedMaterial;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    protected InputLock _inputLock;

    private PlayerStats _playerStats;

    [SerializeField]
    private GameObject _coin;

    public override void Interact()
    {
        _animator.SetTrigger("Cleaning");
        StartCoroutine(CleaningTimer(0.9f));
        base.Interact();
        for (int i = 0; i < _playerStats.CleaningReward; i++)
        {
            Instantiate(_coin, transform.position * (i * 5), quaternion.identity);
        }

    }

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _outlinedMaterial = _spriteRenderer.material;
        _spriteRenderer.material = _defaultMaterial;
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _animator = _playerStats.GetComponent<Animator>();
        _inputLock = _playerStats.GetComponent<InputLock>();
    }

    public override void Highlight()
    {
        _spriteRenderer.material = _outlinedMaterial;
    }

    public override void UnHighlight()
    {
        _spriteRenderer.material = _defaultMaterial;
    }

    IEnumerator CleaningTimer(float timer)
    {
        _inputLock.LockInput();
        yield return new WaitForSeconds(timer);
        _inputLock.UnlockInput();
    }
}
