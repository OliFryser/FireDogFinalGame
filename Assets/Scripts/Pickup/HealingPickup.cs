using UnityEngine;
using FMODUnity;

namespace Pickup
{
    public class HealingPickup : Pickup
    {
        [SerializeField]
        private int _healingAmount;
        
        protected override void HandlePickup()
        {
            RuntimeManager.PlayOneShot("event:/UI/In-GameUI/Pickup_Health");
            if (!_playerStats.FullHealth)
            {
                _playerStats.Heal(_healingAmount);
                base.HandlePickup();
            }
            
        }
    }
}