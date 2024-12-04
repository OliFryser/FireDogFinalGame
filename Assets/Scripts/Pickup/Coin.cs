using FMODUnity;

namespace Pickup
{
    public class Coin : Pickup
    {
        protected override void HandlePickup()
        {
            RuntimeManager.PlayOneShot("event:/UI/In-GameUI/Pickup_money");
            _playerStats.AddCoins(1);
        }
    }
}
