using UnityEngine;

namespace Pickup
{
    public class HealingPickup : Pickup
    {
        [SerializeField]
        private int _healingAmount;
        
        protected override void HandlePickup()
        {
            if (_playerStats.FullHealth)
                _playerStats.Heal(_healingAmount);
        }
    }
}