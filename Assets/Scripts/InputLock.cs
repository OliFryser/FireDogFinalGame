using System;
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
        Debug.Log($"Lock Counter: {_lockCount}");
        if (_lockCount == 1)
            _playerInput.DeactivateInput();
    }

    public void UnlockInput()
    {
        _lockCount--;
        Debug.Log($"Lock Counter: {_lockCount}");
        if (_lockCount == 0)
            _playerInput.ActivateInput();
    }
}
