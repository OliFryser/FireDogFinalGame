using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private Movement _movementScript;

    [SerializeField]
    private GameObject _hitBoxPrefab;

    [SerializeField]
    private float _hitBoxOffset = 0.75f;

    [SerializeField]
    private float _hitBoxDestroyDelay = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        _movementScript = GetComponent<Movement>();
    }

    void OnFire(InputValue _)
    {
        GameObject hitBox;
        if (_movementScript.FacingRight)
        {
            hitBox = Instantiate(_hitBoxPrefab, transform.position + Vector3.right * _hitBoxOffset, quaternion.identity);
        }
        else
        {
            hitBox = Instantiate(_hitBoxPrefab, transform.position + Vector3.left * _hitBoxOffset, quaternion.identity);
        }
        StartCoroutine(DestroyAfterDelay(hitBox));
    }

    IEnumerator DestroyAfterDelay(GameObject hitBox)
    {
        yield return new WaitForSeconds(_hitBoxDestroyDelay);
        Destroy(hitBox);
    }

    void OnEnemyHit()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

}
