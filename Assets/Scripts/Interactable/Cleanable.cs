using System;
using UnityEngine;
using System.Collections;
using FMODUnity;
using SceneManagement;
using Unity.Mathematics;
using Random = System.Random;

public class Cleanable : Interactable
{
    [SerializeField]
    private Material _defaultMaterial;

    private Material _outlinedMaterial;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    private InputLock _inputLock;

    private PlayerStats _playerStats;

    private EnemyTracker _enemyTracker;

    [SerializeField] 
    private Bounds _bounds;

    [SerializeField]
    private GameObject _coin;
    
    [SerializeField]
    private GameObject _fullHeart;

    [SerializeField]
    private GameObject _halfHeart;
    
    public override void Interact()
    {
        _animator.SetTrigger("Cleaning");
        StartCoroutine(CleaningTimer(0.9f));
        base.Interact();

        RuntimeManager.PlayOneShot("event:/Player/Clean_level", transform.position);

        var heartsFromCleaning = 0;
        if (UnityEngine.Random.Range(0, 10) >= 8)
            heartsFromCleaning = Math.Clamp(UnityEngine.Random.Range(1, _playerStats.CleaningReward / 2), 1, 4);
        var coinsFromCleaning = _playerStats.CleaningReward - heartsFromCleaning;
        
        for (int i = 0; i < coinsFromCleaning; i++)
            Instantiate(_coin, GetRandomPositionWithinBounds(), Quaternion.identity);

        for (int i = 0; i < heartsFromCleaning / 2; i++)
            Instantiate(_fullHeart, GetRandomPositionWithinBounds(), Quaternion.identity);
        
        if(heartsFromCleaning % 2 != 0)
            Instantiate(_halfHeart, GetRandomPositionWithinBounds(), Quaternion.identity);
        
        if (_playerStats.CleaningSpreeMoney)
        {
            Instantiate(_coin, GetRandomPositionWithinBounds(), quaternion.identity);
        }
        if (_playerStats.CleaningSpreeDamage && _enemyTracker.EnemyCount > 0)
        {
            _playerStats.CleaningSpreeDamageActive = true;
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
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
    }

    public override void Highlight()
    {
        _spriteRenderer.material = _outlinedMaterial;
    }

    public override void UnHighlight()
    {
        _spriteRenderer.material = _defaultMaterial;
    }

    private IEnumerator CleaningTimer(float timer)
    {
        _inputLock.LockInput();
        yield return new WaitForSeconds(timer);
        _inputLock.UnlockInput();
    }

    private Vector3 GetRandomPositionWithinBounds()
    {
        float x = UnityEngine.Random.Range(_bounds.XMin, _bounds.XMax);
        float y = UnityEngine.Random.Range(_bounds.YMin, _bounds.YMax);
        Debug.Log($"Spawned at X: {x}, Y: {y}");
        return new(transform.position.x + x, transform.position.y + y, transform.position.z);
    }
    
    [Serializable]
    public struct Bounds
    {
        public float XMin, XMax, YMin, YMax; 
    }
}
