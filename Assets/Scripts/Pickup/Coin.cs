namespace Pickup
{
    public class Coin : Pickup
    {
        protected override void HandlePickup()
        {
            _playerStats.AddCoins(1);
        }
    }
}
