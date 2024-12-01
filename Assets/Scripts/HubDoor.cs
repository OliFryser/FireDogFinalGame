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
        RuntimeManager.PlayOneShot("event:/Environment/HUB_Door_Open");
        _animator.SetTrigger("Open Door");
    }
}
