using UnityEngine;

public class HubManager : MonoBehaviour
{
    private HubDoor _door;

    private void Awake()
    {
        _door = FindAnyObjectByType<HubDoor>();
    }

    private void Start()
    {
        _door.OpenDoor();
    }
}
