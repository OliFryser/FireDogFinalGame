using UnityEngine;

public class CouchEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform _playerRef;

    private int health = 30;

    //public float MovementSpeed = 0.75f;

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
        /*Vector2 direction = _playerRef.transform.position - gameObject.transform.position;
        direction.Normalize();

        transform.Translate(direction * MovementSpeed * Time.deltaTime);*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DustEnemy")){
            health -= 10;
            Debug.Log("got hit");
        }
        else{
            health -= 5;
        }
        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.SendMessage("OnEnemyHit");
        else if (other.gameObject.CompareTag("DustEnemy")){
            health -= 10;
            Debug.Log("got hit");
            other.gameObject.SendMessage("StopPush");
        }
    }
}
