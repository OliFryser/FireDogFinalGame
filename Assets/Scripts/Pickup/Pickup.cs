using Player;
using UnityEngine;

namespace Pickup
{
    public abstract class Pickup : MonoBehaviour
    {
        protected PlayerStats _playerStats;

        private void Start()
        {
            _playerStats = FindAnyObjectByType<PlayerStats>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                HandlePickup();
                Destroy(gameObject);
            }
        }
        
        protected abstract void HandlePickup();
    }
}