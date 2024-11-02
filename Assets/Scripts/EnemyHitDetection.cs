using UnityEngine;
using FMODUnity;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    EventReference Enemyhit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Weapon Hit Box"))
        {
            _health--;
            RuntimeManager.PlayOneShot(Enemyhit);
        }
    }
}
