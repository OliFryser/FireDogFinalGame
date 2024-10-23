using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _playerRef;

    public float MovementSpeed = 0.75f;

    void Start()
    {
        _playerRef = FindAnyObjectByType<Movement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = _playerRef.transform.position - gameObject.transform.position;
        direction.Normalize();

        transform.Translate(direction * MovementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.SendMessage("OnEnemyHit");
    }
}
