namespace Merchant
{
    public class Merchant : MerchantBase
    {
        private RoomManager _roomManager;

        protected override void Start()
        {
            base.Start();
            _roomManager = FindFirstObjectByType<RoomManager>();
        }


        public override void Interact()
        {
            base.Interact();
            _roomManager.OpenUpgradeMenu();
        }
    }
}
