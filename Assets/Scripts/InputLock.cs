using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputLock : MonoBehaviour
{
    private PlayerInput _playerInput;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

    }

    public void LockInput()
    {
        _playerInput.DeactivateInput();
    }

    public void UnlockInput()
    {
        _playerInput.ActivateInput();
    }
}
