using UnityEngine;

public class CouchEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform _playerRef;

    private int health = 30;


    void Start()
    {
        _playerRef = FindAnyObjectByType<Movement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            Destroy(gameObject);
        }
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DustEnemy")){
            health -= 10;
        }
        else{
            health -= 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.SendMessage("OnEnemyHit");
        else if (other.gameObject.CompareTag("DustEnemy")){
            health -= 10;
            other.gameObject.SendMessage("StopPush");
        }
    }
}
