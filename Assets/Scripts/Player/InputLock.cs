using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputLock : MonoBehaviour
{
    private PlayerInput _playerInput;
    private int LockCount { get; set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void LockInput()
    {
        LockCount++;

        if (LockCount == 1 && _playerInput)
        {
            _playerInput.actions.FindActionMap("Player", true).Disable();
        }
    }

    public void UnlockInput()
    {
        LockCount--;

        if (LockCount == 0 && _playerInput)
            _playerInput.actions.FindActionMap("Player", true).Enable();
        else if (LockCount < 0)
            LockCount = 0;
    }
}
