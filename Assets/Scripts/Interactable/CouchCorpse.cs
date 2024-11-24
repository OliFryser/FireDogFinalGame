using UnityEngine;
using Unity.Mathematics;

public class CouchCorpse : CleaningInteract
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject _dustEnemy;

    private float _spawnTimer;

    [SerializeField]
    private float _spawnRate;

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (!_hasCleaned){
            if (_spawnTimer >= _spawnRate){
                Instantiate(_dustEnemy, transform.position, quaternion.identity);
                _spawnTimer = 0f;
            }
            else {
                _spawnTimer += Time.deltaTime;
            }

        }
    }


    public override void Interact()
    {
        base.Interact();

    }
}
