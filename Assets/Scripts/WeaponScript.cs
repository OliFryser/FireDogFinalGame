using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    private Movement _movementScript;

    public bool _heavy = false;

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

    void OnFire (InputValue x)
    {
        GameObject hitBox;
        Debug.Log("Facing up?: " + _movementScript.FacingUp.ToString());
        if (_movementScript.FacingRight)
        {
            hitBox = Instantiate(_hitBoxPrefab, transform.position + Vector3.right * _hitBoxOffset, quaternion.identity);
        }
        else if (!_movementScript.FacingRight)
        {
            hitBox = Instantiate(_hitBoxPrefab, transform.position + Vector3.left * _hitBoxOffset, quaternion.identity);
        }
        else if (_movementScript.FacingUp) {
            Debug.Log("This happens?");
            hitBox = Instantiate(_hitBoxPrefab, transform.position + Vector3.up * _hitBoxOffset, quaternion.identity);
        }
        else {
            Debug.Log("Or This happens?");
            hitBox = Instantiate(_hitBoxPrefab, transform.position + Vector3.down * _hitBoxOffset, quaternion.identity);
        }
        StartCoroutine(DestroyAfterDelay(hitBox));
    }

    void OnHeavyHit (InputValue z) {
        _heavy = true;
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
        _heavy = false;
    }

    void OnEnemyHit()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }


}
