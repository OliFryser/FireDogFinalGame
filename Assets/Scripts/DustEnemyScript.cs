using UnityEngine;

public class DustEnemy : MonoBehaviour
{
    private Transform _playerRef;

    private Movement _movementScript;

    private WeaponScript weapon;

    public float MovementSpeed = 0.75f;

    private bool isPushed = false;

    private float pushDistance = 0f;

    public float pushSpeed = 10f;

    Vector2 direction;

    void Start()
    {
        _playerRef = FindAnyObjectByType<Movement>().transform;
        weapon = FindAnyObjectByType<WeaponScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPushed) {
            if (pushDistance < 1.5f) {
                transform.Translate(direction * pushSpeed * Time.deltaTime);
                pushDistance += Time.deltaTime;
            }
            else {
                isPushed = false;
                pushDistance = 0f;
            }
        }
        else {
        direction = _playerRef.transform.position - gameObject.transform.position;
        direction.Normalize();

        transform.Translate(direction * MovementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   

        
        //Debug.Log("Dust hit");
        if (weapon._heavy) {
            getPushedBack();
        } else 
        {
        Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.SendMessage("OnEnemyHit");
    }

    public void getPushedBack () {
        isPushed = true;
        direction = gameObject.transform.position - _playerRef.transform.position;
        direction.Normalize();
    }

    public void StopPush() {
        isPushed = false;
    }
}