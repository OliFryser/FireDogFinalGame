using UnityEngine;
using Unity.Mathematics;
using System.Collections;

public class CouchCorpse : Cleanable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject _dustEnemy;


    private float _spawnTimer;

    [SerializeField]
    private float _spawnRate;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        /*if (!_hasInteracted)
        {
            if (_spawnTimer >= _spawnRate)
            {
                Instantiate(_dustEnemy, transform.position, quaternion.identity);
                _spawnTimer = 0f;
            }
            else
            {
                _spawnTimer += Time.deltaTime;
            }

        }*/
    }


    public override void Interact()
    {
        base.Interact();
        StartCoroutine(DestroyCorpse(0.9f));
    }

    IEnumerator DestroyCorpse(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}