using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private UpgradeMenu _upgradeMenu;

    public void ClearRoom()
    {
        _upgradeMenu.Show();
    }
}
