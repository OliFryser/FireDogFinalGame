using UnityEngine;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _health--;
        }
    }
}
