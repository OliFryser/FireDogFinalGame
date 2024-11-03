using UnityEngine;
//TODO rewrite when needed
public class DustEnemy : MonoBehaviour
{
    private Transform _playerTransform;

    private Weapon _weapon;

    public float MovementSpeed = 0.75f;

    private bool _isPushed = false;

    private float _pushDistance = 0f;

    public float pushSpeed = 10f;

    Vector2 direction;

    void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
        _weapon = FindAnyObjectByType<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPushed)
        {
            if (_pushDistance < 1.5f)
            {
                transform.Translate(direction * pushSpeed * Time.deltaTime);
                _pushDistance += Time.deltaTime;
            }
            else
            {
                _isPushed = false;
                _pushDistance = 0f;
            }
        }
        else
        {
            direction = _playerTransform.transform.position - gameObject.transform.position;
            direction.Normalize();
            transform.Translate(MovementSpeed * Time.deltaTime * direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (_weapon._heavyAttack)
        {
            GetPushedBack();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.SendMessage("OnEnemyHit");
    }

    public void GetPushedBack()
    {
        _isPushed = true;
        direction = gameObject.transform.position - _playerTransform.transform.position;
        direction.Normalize();
    }

    public void StopPush()
    {
        _isPushed = false;
    }
}