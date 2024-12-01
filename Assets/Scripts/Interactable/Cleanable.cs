using UnityEngine;
using System.Collections;
using FMODUnity;
using SceneManagement;
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

    protected EnemyTracker _enemyTracker;

    [SerializeField]
    private GameObject _coin;
    private float _coinOffset = .15f;


    public override void Interact()
    {
        _animator.SetTrigger("Cleaning");
        StartCoroutine(CleaningTimer(0.9f));
        base.Interact();

        RuntimeManager.PlayOneShot("event:/Player/Clean_level", transform.position);

        var coinsFromCleaning = UnityEngine.Random.Range(_playerStats.MinimumCleaningReward, _playerStats.MaximumCleaningReward);

        for (int i = 0; i < coinsFromCleaning; i++)
        {
            Vector3 offset = new Vector3(_coinOffset * i, .5f, 0);
            if (i % 2 == 0)
                Instantiate(_coin, transform.position + offset, quaternion.identity);
            else
                Instantiate(_coin, transform.position - offset, quaternion.identity);
        }
        if (_playerStats.CleaningSpreeMoney)
        {
            CleaningSpreeBonus();
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

    IEnumerator CleaningTimer(float timer)
    {
        _inputLock.LockInput();
        yield return new WaitForSeconds(timer);
        _inputLock.UnlockInput();
    }

    protected void CleaningSpreeBonus()
    {
        if (_enemyTracker.EnemyCount > 0)
        {
            Vector3 offset = new Vector3(_coinOffset, .5f, 0);
            Instantiate(_coin, transform.position + offset, quaternion.identity);
        }
    }
}
