using FMODUnity;
using UnityEngine;

public class HubDoor : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.SetTrigger("Open Door");
    }
}
