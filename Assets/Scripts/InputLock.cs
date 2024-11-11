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
        Debug.Log("Input Locked");
        _playerInput.DeactivateInput();
    }

    public void UnlockInput()
    {
        Debug.Log("Input Unlocked");
        _playerInput.ActivateInput();
    }
}
