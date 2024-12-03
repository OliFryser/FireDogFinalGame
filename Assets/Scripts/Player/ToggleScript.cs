using Player;
using UnityEngine;

public class ToogleScript : MonoBehaviour
{
    private PlayerStats _playerStats;
    private SpriteRenderer _spriteRenderer;

    private 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStats.IsDead) {
            _spriteRenderer.enabled = true;
        }
    }
}
