using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    
    PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        if(_playerController == null)
            Debug.LogError("PlayerController is missing from the GameObject");
    }
    private void OnEnable()
    {
        InputManager.actionMap.PlayerInput.Bomb.started += OnBomb;
    }

    private void OnDisable()
    {
        InputManager.actionMap.PlayerInput.Bomb.started -= OnBomb;
    }
    
    private void OnBomb(InputAction.CallbackContext context)
    {
        _playerController.PlaceBomb();
    }
}
