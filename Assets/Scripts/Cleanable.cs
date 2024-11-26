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

    protected Movement _playerMovement;

    private PlayerStats _playerStats;

    [SerializeField]
    private GameObject _coin;

    public override void Interact()
    {
        _animator.SetTrigger("Cleaning");
        StartCoroutine(CleaningTimer(0.9f));
        base.Interact();
        for (int i = 0; i < _playerStats.CleaningReward; i++){
            Instantiate(_coin, transform.position * (i*5), quaternion.identity);
        }
       
    }

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _outlinedMaterial = _spriteRenderer.material;
        _spriteRenderer.material = _defaultMaterial;
        _animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        _playerMovement = GameObject.FindWithTag("Player").GetComponent<Movement>();
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

    IEnumerator CleaningTimer(float timer){
        _playerMovement.canMove = false;
        yield return new WaitForSeconds(timer);
        _playerMovement.canMove = true;
    }
}
