using UnityEngine;
using UnityEngine.InputSystem;

public class InputLock : MonoBehaviour
{
    private PlayerInput _playerInput;
    private int _lockCount;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void LockInput()
    {
        _lockCount++;

        if (_lockCount == 1 && _playerInput != null)
            _playerInput.actions.FindActionMap("Player", true).Disable();
    }

    public void UnlockInput()
    {
        _lockCount--;

        if (_lockCount == 0 && _playerInput != null)
            _playerInput.actions.FindActionMap("Player", true).Enable();
        else if (_lockCount < 0)
            _lockCount = 0;
    }

    public int GetLockCount(){
        return _lockCount;
    }
}
