using UnityEngine;
using Unity.Mathematics;

public class CouchCorpse : Cleanable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject _dustEnemy;

    private EnemyTracker _enemyTracker;

    private float _spawnTimer;

    [SerializeField]
    private float _spawnRate;

    void Start(){
        base.Start();
        _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        _enemyTracker.RegisterEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (!_hasInteracted){
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
        Destroy(gameObject);
        _enemyTracker.UnregisterEnemy();
        

    }
}